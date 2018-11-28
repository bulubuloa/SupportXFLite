# SupportXFLite

A simple MVVM Framework for Xamarin Forms, it helps you to quick setup a new Xamarin Forms project. I used this library in my personal project and my company also every day :)

Available on NuGet: [![NuGet](https://img.shields.io/badge/nuget%20supportwidgetxf-v1.2.0-blue.svg)](https://www.nuget.org/packages/SupportWidgetXF/)

Add assembly references

    xmlns:ultimateChart="clr-namespace:SupportWidgetXF.Widgets;assembly=SupportWidgetXF"

Setup for iOS project (add to AppDelegate before LoadApplication)

    SupportWidgetXFSetup.Initialize();

Setup for Android project (add to MainActivity before LoadApplication)

    SupportWidgetXFSetup.Initialize(this);
## Support Widget Package

 - SupportAutoComplete **(Complete)**
 - SupportResultList **(Complete)**
 - SupportDropList **(Complete)**
 - SupportEntry **(Complete)**
 - SupportButton  **(Complete)**
 - SupportActionMenu  **(Complete)**
 - SupportBindableStackLayout  **(Complete)**
 - SupportFlowLayout  **(Complete)**
 - SupportSearchView  **(Complete)**
 - SupportShadowView  **(Complete)**
 - SupportGradientView  **(Complete)**
 - SupportMapView  **(Complete)**
 - SupportRadioButton  **(Complete)**
 - SupportCalendarView  **(Complete)**
  
<table>
	<tr>
		<td>Controls</td>
		<td>Screenshots</td>
	</tr>
	<tr>
		<td>
			<b>SupportAutocomplete</b> with 4 row templates: support binding Itemsource, etc..
			<ul>
				<li>Single Title</li>
				<li>Title With Description</li>
				<li>Icon with Title</li>
				<lip>FullText with Icon</li>
				<li>Autocomplete source from API</li>
			</ul>
		</td>
		<td><img src="https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_autocomplete.gif" width="324" height="639" /></td>
	</tr>
	<tr>
		<td>
			<b>SupportDropList</b> with 4 row templates: support binding Itemsource, multi select
			<ul>
				<li>Single Title</li>
				<li>Title With Description</li>
				<li>Icon with Title</li>
				<lip>FullText with Icon</li>
				<li>Autocomplete source from API</li>
			</ul>
		</td>
		<td><img src="https://github.com/bulubuloa/SupportWidgetXF/blob/master/ScreenShots/demo_droplist.gif" width="300" height="472" /></td>
	</tr>
</table>
