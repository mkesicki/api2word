# API2Word

This is simple C# script to parse Postman Collection (via their API) and store it to Word (.docx) file.
It is super basic and far from perfect ... and yes I am not C# developer (this is why I chose this language to play).
Feel free to play, change, complain and improve ... please note this script is as it is and there is no warranty :)

## Why Word

I needed (for work) to store documentation from Postman in Word and after a lot of copy & pasting I decided ... never again :)

Yes, I know, Postman team is working to prepare export of docs to PDF and there are other scripts which do similar things.

## Used Packages
- DocX
- RestSharp
- YamlDotNet

## Usage 
```
$ ./Api2Word.exe
You need to pass 3 parameters:
collection type
path to config file
name of Collection to parse
```
```
$ ./Api2Word.exe postman "..\..\config\config.yaml" "DocuWare REST Samples" Word
```
## Example response
```
Read Config File: "..\..\config\config.yaml"
Get collection: DocuWare REST Samples
Found collection ID: 00a42a46-d8b3-4437-996a-e8a0443ffeca
Search for collection with id: 00a42a46-d8b3-4437-996a-e8a0443ffeca
Number of collections' endpoints: 10
Create file: D:\api2word\Api2Word\bin\Debug\results\DocuWare REST Samples.docx
Get list of endpoints
Parse endpoint: Forms Authentication
Endpoint response: Response OK
Endpoint response: Response 401 Unathorized
Parse endpoint: Get organization
Endpoint response: Response
Parse endpoint: Get all file cabinets and baskets
Endpoint response: Response
Parse endpoint: Get all data for a file cabinet (e.g fields)
Parse endpoint: Check-in a particular document from file system
Parse endpoint: Check-out a particular document and download it
Endpoint response: Response
Parse endpoint: Download thumbnail
Parse endpoint: Download document
Parse endpoint: Get documents from a file cabinet
Parse endpoint: Get first 5 documents from a file cabinet
Endpoint response: Response
Parse endpoint: Get first document from a file cabinet with two fields only
Endpoint response: Response
Parse endpoint: Get a particular document
Endpoint response: Response
Parse endpoint: Get all dialogs of the file cabinet
Endpoint response: Response
Parse endpoint: Get the search dialog
Endpoint response: Response
Parse endpoint: Get total amount of documents
Parse endpoint: Get all dialogs of the file cabinet
Endpoint response: Response
Parse endpoint: Get the search dialog
Endpoint response: Response
Parse endpoint: Build the URL to use in GET request
Endpoint response: Created URL
Parse endpoint: Use generated URL
Parse endpoint: Get a particular document
Endpoint response: Response
Parse endpoint: Update index values (JSON)
Endpoint response: Response
Parse endpoint: Update index values (XML)
Endpoint response: Response
Parse endpoint: Create a new document in a file cabinet
Parse endpoint: Upload  a single pdf document
Endpoint response: Response
Save file: XXX\results\DocuWare REST Samples.docx
```

