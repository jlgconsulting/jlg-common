# Jlg.Common

In order to use these common libraries: 
<br/>
<b>1)</b> Clone this repository inside your project directory (This will create the folder "Jlg.Common")
<br/>
<b>2)</b> Add this line to your <b>.gitignore file</b> from your repository:
<br/>
<b>Jlg.Common/</b>
<br/>
<br/>
<i>This will make Jlg.Common a distinct repository within your own project repository, so that each time you add new functionality to Jlg.Common, 
the changes will be committed separately to Jlg.Common repository and not to your project repository. </i>
<br/>
<br/>
<b>3)</b> Open JlgCommon.sln, restore Nuget packages and build solution
<br/>
<br/>
Because now Nuget packages restore doesn't work anymore, in both projects JlgCommon and JlgCommonTests, you must install manually from Package Manager Console:
<br/>
<i>Install-Package SpreadsheetLight</i>
<br/>
<i>Install-Package DocumentFormat.OpenXml -Version 1.0.0</i>
<br/>
<br/>
<b>4)</b> Inside Visual Studio, right click on Solution -> Add -> Existing Project -> choose "Solution Files (*.sln)" -> \Jlg.Common\JlgCommon.sln
<br/>
<br/>
This will include both JlgCommon and JlgCommonTests projects into your solution.
