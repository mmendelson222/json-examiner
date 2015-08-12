# json-examiner
This is a .NET based "quick and dirty" JSON tool that I wrote for Moshe. 

The user can select a JSON file from local file system (doesn't read from the web presently).  
The file is parsed into a tree view, where data can be browsed.  Righ-click on tree nodes for additional options. 

Most important is Export, which allows you to dump data into a tab-delimited format for convenient import to Excel.
It comes with some limitations:
* Only arrays of objects can be exported.  If values or arrays are present at the top level, they will be ignored.
* Export will consist of the properties of the objects in the array.   Only VALUE properties will be exported (not array or objects).
* The first element is used to determine the list of properties to export.  Those properties are then assumed to be present for all objects in the top level array.


