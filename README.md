# interview-cloud

1. Ensure that your local machine supports .NET Framework v4.6.1
1. Clone this repository to your local machine
1. Open the Solution CloudStatus.sln in Visual Studio
1. Run the Integration Tests and ensure they pass
1. Using Postman, open the collection  CloudStatusRequests.postman_collection.json
1. Execute each of the GET requests to ensure that you are retrieving the pre-populated data
1. Modify the body of the POST request to write a few Server Status transactions
1. Run the GET requests again and verify that the averages have changed for the hours or minutes for which you submitted new data