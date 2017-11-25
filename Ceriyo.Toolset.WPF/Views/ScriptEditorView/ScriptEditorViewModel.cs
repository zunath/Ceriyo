using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Contracts;
using Ceriyo.Core.Extensions;
using Ceriyo.Core.Scripting;
using MonoGame.Extended.Collections;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Ceriyo.Toolset.WPF.Views.ScriptEditorView
{
    public class ScriptEditorViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScriptService _scriptManager;

        public ScriptEditorViewModel(IScriptService scriptManager,
            IEventAggregator eventAggregator)
        {
            _scriptManager = scriptManager;
            _eventAggregator = eventAggregator;
            Constants = new ObservableCollection<string>();
            ScriptText = string.Empty;
            
            LoadHelperBox();

            CopyMethodTextCommand = new DelegateCommand(CopyMethodText);
            CopyConstantTextCommand = new DelegateCommand(CopyConstantText);
            ValidateScriptCommand = new DelegateCommand(ValidateScript);
            NewScriptCommand = new DelegateCommand(NewScript);
            SaveScriptCommand = new DelegateCommand(SaveScript);
            OpenScriptCommand = new DelegateCommand(OpenScript);
        }

        private ObservableCollection<ScriptMethod> _methods;

        public ObservableCollection<ScriptMethod> Methods
        {
            get => _methods;
            set => SetProperty(ref _methods, value);
        }

        private ScriptMethod _selectedMethod;

        public ScriptMethod SelectedMethod
        {
            get => _selectedMethod;
            set
            {
                SetProperty(ref _selectedMethod, value);
                HelpText = BuildHelpText(_selectedMethod);
            }
        }

        private ObservableCollection<string> _constants;

        public ObservableCollection<string> Constants
        {
            get => _constants;
            set => SetProperty(ref _constants, value);
        }

        private string _selectedConstant;

        public string SelectedConstant
        {
            get => _selectedConstant;
            set => SetProperty(ref _selectedConstant, value);
        }

        public DelegateCommand CopyMethodTextCommand { get; set; }

        private void CopyMethodText()
        {
            // TODO: Insert text at the caret.
        }

        public DelegateCommand CopyConstantTextCommand { get; set; }

        private void CopyConstantText()
        {
            // TODO: Insert text at the caret.
        }

        private string _helpText;

        public string HelpText
        {
            get => _helpText;
            set => SetProperty(ref _helpText, value);
        }

        private string _scriptText;

        public string ScriptText
        {
            get => _scriptText;
            set => SetProperty(ref _scriptText, value);
        }

        private int _caretOffset;

        public int CaretOffset
        {
            get => _caretOffset;
            set => SetProperty(ref _caretOffset, value);
        }


        private void LoadHelperBox()
        {
            BuildScriptMethods();

            var constants = _scriptManager.GetRegisteredEnumerations().ToList();
            foreach (var constant in constants)
            {
                Constants.Add(constant);
            }
        }

        private void BuildScriptMethods()
        {
            List<ScriptMethod> methodList = new List<ScriptMethod>();
            const string xmlDocumentationFilePath = ".\\Content\\Data\\ScriptData.xml";

            var methodTypes = Assembly.GetAssembly(typeof(IScriptService)).GetTypes()
                .Where(t => string.Equals(t.Namespace, "Ceriyo.Core.Scripting.Server", StringComparison.Ordinal))
                .ToArray();

            string[] ignoreMethods = { "ToString", "Equals", "GetHashCode", "GetType" };

            foreach (var methodType in methodTypes)
            {
                string @namespace = ((ScriptNamespaceAttribute)methodType.GetCustomAttribute(typeof(ScriptNamespaceAttribute))).Namespace;

                var methods = methodType.GetMethods();
                foreach (var method in methods)
                {
                    if (ignoreMethods.Contains(method.Name)) continue;

                    ScriptMethod scriptMethod = new ScriptMethod
                    {
                        Name = @namespace + ":" + method.Name,
                        Returns = method.ReturnParameter.GetXmlDocumentation(xmlDocumentationFilePath),
                        Summary = method.GetXmlDocumentation(xmlDocumentationFilePath)
                    };

                    foreach (var param in method.GetParameters())
                    {
                        string paramDocumentation = param.GetXmlDocumentation(xmlDocumentationFilePath);
                        scriptMethod.Parameters.Add(paramDocumentation);
                    }

                    scriptMethod.Prototype = $"{scriptMethod.Name}(";
                    int paramCount = method.GetParameters().Length;

                    for (int index = 0; index < paramCount; index++)
                    {
                        var param = method.GetParameters()[index];
                        scriptMethod.Prototype += $"{ConvertCSharpTypeToLuaType(param.ParameterType)} {param.Name}";
                        if (index + 1 < paramCount)
                        {
                            scriptMethod.Prototype += ", ";
                        }

                    }
                    scriptMethod.Prototype += ")";

                    methodList.Add(scriptMethod);
                }
            }

            methodList = methodList.OrderBy(x => x.Name).ToList();
            Methods = new ObservableCollection<ScriptMethod>(methodList);
        }

        private static string ConvertCSharpTypeToLuaType(Type type)
        {
            if (type.IsEnum) return type.Name;
            string typeName = type.Name;

            switch (typeName)
            {
                case "Int32":
                case "Single":
                case "Double":
                    return "number";
                case "String":
                    return "string";
                case "Boolean":
                    return "boolean";
                default:
                    return "object";
            }
        }

        private string BuildHelpText(ScriptMethod method)
        {
            string helpText = $"Summary:   {method.Summary}\n";
            if (!string.IsNullOrWhiteSpace(method.Returns))
                helpText += $"Returns:   {method.Returns}\n";

            helpText += $"{ method.Prototype}\n";

            return helpText;
        }

        public DelegateCommand ValidateScriptCommand { get; set; }

        private void ValidateScript()
        {
            string result = _scriptManager.ValidateScript(ScriptText);

            HelpText = string.IsNullOrWhiteSpace(result) ?
                "Script compiled successfully." :
                result;
        }

        public DelegateCommand NewScriptCommand { get; set; }

        private void NewScript()
        {
            ScriptText = string.Empty;
        }

        public DelegateCommand SaveScriptCommand { get; set; }

        private void SaveScript()
        {
        }

        public DelegateCommand OpenScriptCommand { get; set; }

        private void OpenScript()
        {

        }

    }
}
