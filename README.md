# opendata

Parser of Govement Open Data Sites

# Process wrokflow

Extract list of data sets

> Opendata.Process.GrabDataSets.exe "pathToDatabase\opendata.db" "region code" "region name" "http://site/list.csv" 

Load medata files

> Opendata.Process.GrabMeatadata.exe "pathToDatabase\opendata.db"

Load and parse data structure

> Opendata.Process.GrabStructure.exe "pathToDatabase\opendata.db"

Load and parse data instance

> Opendata.Process.GrabData.exe "pathToDatabase\opendata.db"
