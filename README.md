# SocioMatrix
Takes raw data and creates a sociomatrix from it.

Load accepts any file, but it is hard-coded to expect tab separated values. The first line will be the headers. Each line after a new row matching the headers.

Calculate will determine similarities for each row item. A square matrix showing all similarities between header types is then produced. 

Save will save the sociomatrix if it's been calculated as tab separated values.

Note: Performance is not optimal for this application. There is room for improved algorithms or multi-threading.