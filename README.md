# Starbucks Web Locator

##Introduction##
This is a real time application wherein the user can see if a location is good to start a Starbucks or not. 
The decision is made based on the classifier result, the classifier considers the top 20 attributes around that location and 
based on these values the classifier determines if the Starbucks at that particular position is beneficial or not. 

This web application also provides the user with the access to retrieve the Starbucks location and address in google maps. 
It likewise provides the map in satellite view and terrain view (map view). 
The Starbucks data is a real time dataset with its address, latitude and longitude  information. 

##Project Environment##
* PROGRAMMING LANGUAGE  :  .NET(C#, Asp.NET), Java, J48 Classifier
* DATABASE              :  Microsoft SQL Server 2012
* TOOL 				          :  Microsoft Visual Studio 2012
* DLL FILES             :  Gmaps.DLL, IKVM, 

##Implementation##
* The Starbucks data is collected from the web: https://opendata.socrata.com/Business/All-Starbucks-Locations-in-the-US-Map/ddym-zvjk 
* This data is cleaned and top attributes are found using data mining techniques.
* A classifier is built in java using J48 classifier jar and this code is converted to .dll file using IKVM.
* We used this .dll file in out .Net project and developed a web application.

