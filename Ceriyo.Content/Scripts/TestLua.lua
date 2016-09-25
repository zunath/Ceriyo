function Main()
	Logging:Print("Firing from lua");

	local desktop = Control:CreateNewDesktop();
	local window = Control:CreateWindow(200, 200, 20, 20, "the title2", true);

	Control:AddWindowToDesktop(desktop, window);
	Control:ChangeDesktop(desktop);

end 