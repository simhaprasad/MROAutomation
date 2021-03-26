Coded UI Test framework to query and operate desktop user-interface(UI) elements, by using "selector text" similar to jQuery, without UIMap. 
You can use it for "Test Driven Development". You can create UI test code more quickly and easily in combination of UIMap and this framework.

Note: Visual Studio 2013 or 2015

    Change App.config bindingRedirect newVersion:
		- 12.0.0.0 for Visual Studio 2013
		- 14.0.0.0 for Visual Studio 2015 (Default)

      <dependentAssembly>
        <assemblyIdentity name="Microsoft.VisualStudio.QualityTools.CodedUITestFramework" publicKeyToken="b03f5f7f11d50a3a" />
        <bindingRedirect oldVersion="10.0.0.0-14.0.0.0" newVersion="14.0.0.0" />
      </dependentAssembly>

How to use CodedUITestPlus

1. Create "Coded UI Test" project on Visual Studio 2015 Enterprise, 2013 Premium or Ultimate.
2. Add "CodedUITestPlus" reference by using NuGet package manager.
3. Add using (Imports in Visual Basic) to use extensions.

	using System.Linq;
	using System.Collections.Generic;
	using Net.Surviveplus.CodedUIQuery;
	using Net.Surviveplus.CodedUITestPlus;

4. Code UI Test don't launch your application in default.
	Call App.LaunchSolutionProject in the test method, to launch target application.

	Sample:
		using (var app = App.LaunchSolutionProject( this, "SampleWpfApp" )) {

			// Coded UI Test by using UIMap.
			this.UIMap.AddNewItem1();
			this.UIMap.AssertListItemCount1();
		} 

	The application will be closed by App.Dispose(), like as exiting from using block.

	This code will work on following solution and projects in this sample.
	Set project dependencies. 	

		YourSolutionName.sln (Visual Studio Solution )
			+ SampleWpfApp (WPF application project)
			+ SampleWpfApp.Test (Coded UI Test project)

		Project build order : SampleWpfApp.Test, SampleWpfApp 

5. Confirm target element Window Handle, process id, class name, "AutomationId" or x:Name in WPF.
	You can see AutomationId in UIMap.uitest (Window) > UI Control Map > Properties (Tool Window) > Search Properties.
	You can also use Spy++ in Visual Studio Tools, or UI Spy in Microsoft Windows SDK.

	Define the selector text, and create query, like as this sample.

	Selector Format: 
		#handle<ProcessName>{ClassName}[WindowTitle](xaml x:Name) 
		Use comma separated selector to use OR condition.

	Sample:
		// You can find window of launched app, and click the button that XAML element x:Name is "textButton".
		App.Element.Find( "(textButton)" ).MoveMouseTo().Click()

	see: CodedUIQuery README.txt
	
6. Add "OperationAssert" method and query by using "CodedUIQuery" to implement additional test.

	The query of CodedUIQuery is lazy evaluation.
	To force immediate execution of any query and cache its results, 
	and operate such as Click method, you can call the ToLis<TSource> or ToArray<TSource> methods.
	OperationAssert executes query and verifies result of it, in test methods.

	Sample:
		using (var app = App.LaunchSolutionProject( this, "SampleWpfApp" )) {

			OperationAssert.TextIs(
				"Home page is displayed first within 0.5 seconds.",
				"HOME",
				app.Element.Sleep( TimeSpan.FromSeconds( 0.5 ) ).Find( "(pageTitle)" ) );

			OperationAssert.TextIs(
				"You can input a text into Name. ("Sample Name" is displayed first.)",
				"Sample Name 1",
				app.Element.Find( "(name)" ).MoveMouseTo().SendKeys( "{END}" ).InputText( " 1" ).Sleep( TimeSpan.FromSeconds( 0.5 ) ) );

			// Coded UI Test by using UIMap.
			this.UIMap.AddNewItem1();
			this.UIMap.AssertListItemCount1();

		} // end using(app)

	You can create all of UI test code by using "CodedUIQuery" without "UIMap".
	But, we recommend that you use in combination them.
	UIMap is more powerful. 	You can use CodedUIQuery for "Test Driven Development".

	see: OperationAssert.


How to convert UITestControl into IOperationElement

	Use Query extension method.
	You can find the element which is not defined in UIMap, from UIMap elements.

	Sample:
		OperationAssert.TextIs( 
			"Home page is displayed.", 
			"HOME", 
			UIMap.UIMainWindowWindow.Query().Find( "(pageTitle)" ) );


How to convert IOperationElement into UITestControl

	Use ToUITestControl extension method.
	You can use UITestControl features that there is not in CodedUIQuery.
	But, because the part does not have compatibility, it may not work.

	Sample:
		Mouse.Click( 
			Desktop.Elements.Children( "[YourAppTitle]" ).Find( "(textButton)" ).First().ToUITestControl(), 
			new Point(0,0) );


How to Test web app
    Use App.LaunchWebServer or LaunchSolutionWebServer methods.

    Sample:
        using (var iis = App.LaunchSolutionWebServer( this, "SampleWebApp", 12590))
        using (var app = App.LaunchWellKnownApp(WellKnownApp.InternetExplorer, iis.Url))
        {

			// Assert html text
            var result = app.GetHtmlText("#qunit-testresult .failed");
            Assert.AreEqual("0", result, "QUnit JavaScript unit testing failed.");

			// Assert by using mshtml.HTMLDocument object.
            var title = app.Html.getElementsByTagName("h1").item(0).InnerHtml as string;
            Assert.AreEqual("TypeScript HTML App", title, "h1 title is wrong.");

            var value = app.Html.getElementById("id").getAttribute("value") as string;
            Assert.AreEqual("Expected Value", value);

			// Operation 
			app.ExecuteJavaScript("$('#buttonId').click();");

			// Coded UI Test by using UIMap
			this.UIMap.AddNewItem1();
			this.UIMap.AssertListItemCount1();

        } // end using iis, app


How to record screen to a video file 

	You can record screen to video file and attach it to the test result. 
	This is  beta feature.

	1. Install Microsoft Expression Encoder 4.

		Download free edition from http://www.microsoft.com/en-us/download/details.aspx?id=27870 .
		( Note: Free edition may have the 10 minute recording limit. )

		( Note: Because Microsoft Expression Encoder 4 is not supported in Windows 8 and 8.1,
			we want to update this implementation. But it is required now. 
			We confirmed it works still now in Windows 8.1. )

	2. Call ScreenRecorder.StartRecording in the test method, to start recording.
		Specify ScreenRecordingStyle.SaveIfExceptionThrown, to save video file only when an exception is thrown.

			Sample 1: 
				// Record All Screens.
				using(ScreenRecorder.StartRecording( this, ScreenRecordingStyle.SaveAlways ))
				using(var app = App.LaunchSolutionProject( this, "SampleWpfApp" )) 
				{
					// TODO: Insert test code here.

				} // end using(app, ScreenRecorder)

			Sample 2: 
				// Record App Window only.
				using (var app = App.LaunchSolutionProject( this, "SampleWpfApp" ))
				using (ScreenRecorder.StartRecording( this, app, ScreenRecordingStyle.SaveAlways ))
				{
					// TODO: Insert test code here.

				} // end using(app, ScreenRecorder)

	3. Run "Coded UI Test".

	4. The WMV (Windows Media Video) file is saved in the test result folder, and it is attached to the test result.
		You can open it from the link of the test result.

		( Note: It takes time very much until encoding is completed, in this version. )

	5. If Windows Media Player is not installed in your system, for example Windows 8.1 in Windows Azure gallery,
		download "Media Feature Pack" from  http://www.microsoft.com/en-us/download/details.aspx?id=40744 ,
		to play the WMV file.

	Note: If you want record only primary screen, and record in all of test method, you can use Visual Studio 2013 test settings.
		see: https://msdn.microsoft.com/en-us/library/dd286596(v=vs.120).aspx


Sample Download and Video

	Microsoft Code - C# Japanese sample
	https://code.msdn.microsoft.com/WPF-UI-Coded-UI-Test-9ab5581d

	YouTube - How to record screen to a video file (CodedUITestPlus)
	http://youtu.be/PmRkXV5o4cA