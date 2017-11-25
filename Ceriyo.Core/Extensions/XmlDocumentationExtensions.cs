﻿// Reading XML Documentation at Run-Time
// Bradley Smith - 2010/11/25

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Ceriyo.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for reading XML comments from reflected members.
    /// </summary>
    public static class XmlDocumentationExtensions
    {

        private static readonly Dictionary<string, XDocument> CachedXml;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static XmlDocumentationExtensions()
        {
            CachedXml = new Dictionary<string, XDocument>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the expected name for a member element in the XML documentation file.
        /// </summary>
        /// <param name="member">The reflected member.</param>
        /// <returns>The name of the member element.</returns>
        private static string GetMemberElementName(MemberInfo member)
        {
            char prefixCode;
            string memberName = (member is Type)
                ? ((Type)member).FullName                               // member is a Type
                : (member.DeclaringType.FullName + "." + member.Name);  // member belongs to a Type

            switch (member.MemberType)
            {
                case MemberTypes.Constructor:
                    // XML documentation uses slightly different constructor names
                    memberName = memberName.Replace(".ctor", "#ctor");
                    goto case MemberTypes.Method;
                case MemberTypes.Method:
                    prefixCode = 'M';

                    // parameters are listed according to their type, not their name
                    string paramTypesList = String.Join(
                        ",",
                        ((MethodBase)member).GetParameters()
                        .Select(x => x.ParameterType.FullName
                        ).ToArray()
                    );
                    if (!String.IsNullOrEmpty(paramTypesList)) memberName += "(" + paramTypesList + ")";
                    break;

                case MemberTypes.Event:
                    prefixCode = 'E';
                    break;

                case MemberTypes.Field:
                    prefixCode = 'F';
                    break;

                case MemberTypes.NestedType:
                    // XML documentation uses slightly different nested type names
                    memberName = memberName.Replace('+', '.');
                    goto case MemberTypes.TypeInfo;
                case MemberTypes.TypeInfo:
                    prefixCode = 'T';
                    break;

                case MemberTypes.Property:
                    prefixCode = 'P';
                    break;

                default:
                    throw new ArgumentException("Unknown member type", nameof(member));
            }

            // elements are of the form "M:Namespace.Class.Method"
            return $"{prefixCode}:{memberName}";
        }

        /// <summary>
        /// Returns the XML documentation (summary tag) for the specified member.
        /// </summary>
        /// <param name="member">The reflected member.</param>
        /// <returns>The contents of the summary tag for the member.</returns>
        public static string GetXmlDocumentation(this MemberInfo member)
        {
            AssemblyName assemblyName = member.Module.Assembly.GetName();
            return GetXmlDocumentation(member, assemblyName.Name + ".xml");
        }

        /// <summary>
        /// Returns the XML documentation (summary tag) for the specified member.
        /// </summary>
        /// <param name="member">The reflected member.</param>
        /// <param name="pathToXmlFile">Path to the XML documentation file.</param>
        /// <returns>The contents of the summary tag for the member.</returns>
        public static string GetXmlDocumentation(this MemberInfo member, string pathToXmlFile)
        {
            AssemblyName assemblyName = member.Module.Assembly.GetName();
            XDocument xml;

            if (CachedXml.ContainsKey(assemblyName.FullName))
                xml = CachedXml[assemblyName.FullName];
            else
                CachedXml[assemblyName.FullName] = (xml = XDocument.Load(pathToXmlFile));

            return GetXmlDocumentation(member, xml);
        }

        /// <summary>
        /// Returns the XML documentation (summary tag) for the specified member.
        /// </summary>
        /// <param name="member">The reflected member.</param>
        /// <param name="xml">XML documentation.</param>
        /// <returns>The contents of the summary tag for the member.</returns>
        public static string GetXmlDocumentation(this MemberInfo member, XDocument xml)
        {
            return xml.XPathEvaluate(
                $"string(/doc/members/member[@name='{GetMemberElementName(member)}']/summary)"
            ).ToString().Trim();
        }

        /// <summary>
        /// Returns the XML documentation (returns/param tag) for the specified parameter.
        /// </summary>
        /// <param name="parameter">The reflected parameter (or return value).</param>
        /// <returns>The contents of the returns/param tag for the parameter.</returns>
        public static string GetXmlDocumentation(this ParameterInfo parameter)
        {
            AssemblyName assemblyName = parameter.Member.Module.Assembly.GetName();
            return GetXmlDocumentation(parameter, assemblyName.Name + ".xml");
        }

        /// <summary>
        /// Returns the XML documentation (returns/param tag) for the specified parameter.
        /// </summary>
        /// <param name="parameter">The reflected parameter (or return value).</param>
        /// <param name="pathToXmlFile">Path to the XML documentation file.</param>
        /// <returns>The contents of the returns/param tag for the parameter.</returns>
        public static string GetXmlDocumentation(this ParameterInfo parameter, string pathToXmlFile)
        {
            AssemblyName assemblyName = parameter.Member.Module.Assembly.GetName();
            XDocument xml;

            if (CachedXml.ContainsKey(assemblyName.FullName))
                xml = CachedXml[assemblyName.FullName];
            else
                CachedXml[assemblyName.FullName] = (xml = XDocument.Load(pathToXmlFile));

            return GetXmlDocumentation(parameter, xml);
        }

        /// <summary>
        /// Returns the XML documentation (returns/param tag) for the specified parameter.
        /// </summary>
        /// <param name="parameter">The reflected parameter (or return value).</param>
        /// <param name="xml">XML documentation.</param>
        /// <returns>The contents of the returns/param tag for the parameter.</returns>
        public static string GetXmlDocumentation(this ParameterInfo parameter, XDocument xml)
        {
            if (parameter.IsRetval || String.IsNullOrEmpty(parameter.Name))
                return xml.XPathEvaluate(
                    $"string(/doc/members/member[@name='{GetMemberElementName(parameter.Member)}']/returns)"
                ).ToString().Trim();
            else
                return xml.XPathEvaluate(
                    $"string(/doc/members/member[@name='{GetMemberElementName(parameter.Member)}']/param[@name='{parameter.Name}'])"
                ).ToString().Trim();
        }

    }
}
