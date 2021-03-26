CodedUIQuery is a Library to query and operate desktop user-interface(UI) elements, by using "selector text" similar to jQuery. 
Use this for "Desktop Automation" or "Coded UI Test" with LINQ in C# and Visual Basic.


How to use CodedUIQuery for "Desktop Automation"

1. Add "CodedUIQuery" reference by using NuGet package manager.
2. Add using (Imports in Visual Basic) to use extensions.

	using System.Linq;
	using System.Collections.Generic;
	using Net.Surviveplus.CodedUIQuery;

3. Confirm target element Window Handle, process id, class name, "AutomationId" or x:Name in WPF.
	You can use Spy++ in Visual Studio Tools, or UI Spy in Microsoft Windows SDK.
 
4. Define the selector text, and create query, like as this sample.

	Selector Format: 
		#handle<ProcessName>{ClassName}[WindowTitle](xaml x:Name) 
		Use comma separated selector to use OR condition.

	Sample 1:
		// You can find Notepad and, input "Hello !" into all of the Notepad in this desktop.
		var query = Desktop.Elements.WaitForChildren( "{Notepad}", TimeSpan.FromSeconds( 10 ) ).Children( "{Edit}" ).InputText( "Hello !" );

	Sample 2:
		// You can find window that title text is "YourAppTitle", and click the button that XAML element x:Name is "textButton".
		var query = Desktop.Elements.Children( "[YourAppTitle]" ).Find( "(textButton)" ).Click();

	see: IOperationElement,  IEnumerable<IOperationElement> extensions.

5. These query is lazy evaluation.
	To force immediate execution of any query and cache its results, 
	and operate such as Click method, you can call the ToLis<TSource> or ToArray<TSource> methods.
	Call these methods, like as this sample.

	// Execute and count of result of query.
	var count = query.Count();


How to use CodedUIQuery for "Coded UI Test"

	You can use "CodedUITestPlus" in "Coded UI Test", to use CodedUIQuery.
	OperationAssert executes query and verifies result of it, in test methods.

	Sample:
		using (var app = App.LaunchSolutionProject( this, "SampleWpfApp" )) {

			OperationAssert.TextIs(
				"Home page is displayed first within 0.5 seconds.",
				"HOME",
				app.Element.Sleep( TimeSpan.FromSeconds( 0.5 ) ).Find( "(pageTitle)" ).MoveMouseTo() );

			OperationAssert.TextIs(
				"You can input a text into Name. ("Sample Name" is displayed first.)",
				"Sample Name 1",
				app.Element.Find( "(name)" ).MoveMouseTo().SendKeys( "{END}" ).InputText( " 1" ).Sleep( TimeSpan.FromSeconds( 0.5 ) ) );

		} // end using(app)

	see "CodedUITestPlus" NuGet package.
