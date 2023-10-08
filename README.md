# XMLConverter

This is a console application converting row based files to xml files.<br>
The application requires the inputfile to follow a specific structure. The structure is declared in other documentation.

### To run the application

Unzip the folder XMLConverterApp.zip<br>
Put the row based files to convert to xml in the InputFiles folder.<br>
Open the folder where <i>XMLConverter.exe</i> is located in a terminal and set the the commandline options:
<ul>
  <li><i>--inputfile</i> followd by the name of the row based file to convert to xml </li>
  <li><i>--outputfile</i> followd by the name the generated xml file will have  </li>
</ul>

#### Example (on Windows):
<code>XMLConverter.exe --inputfile people.csv --outputfile people.xml</code>

#### Example (on Mac):
<code>XMLConverter.dll --inputfile people.csv --outputfile people.xml</code>

