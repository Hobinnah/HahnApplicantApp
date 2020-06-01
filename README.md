# HahnApplicantApp
A server, client application
There are two major projects
1. The WebAPI
2. The client


################# WebAPI #########################
TO GET THE API RUNNING

1. Go to a desired folder location
2. Type cmd on the folder address and tab the enter button.
3. Pull project from github using the command below
4. git pull https://github.com/Hobinnah/HahnApplicantApp.git
5. Open project using desired editor.
6. Restore the solution's dependencies.
7. Build and run.
8. Note the port the webapi is running on.


################# WebCLIENT #########################
TO GET THE CLIENT RUNNING

1. Go to a desired folder location
2. Type cmd on the folder address and tab the enter button.
3. Pull project from github using the command below
4. git pull https://github.com/Hobinnah/HahnWebClient.git
5. Open project using desired editor.
6. run npm i, to restore the client's dependencies.
7. run ng build to accertain there are no errors.
8. serve the client by running ng serve.
9. Load app on default port. http://localhost:4200
10. The client web app will load.

NOTE 1: If webapi is running on a different port asides 5000,
	  kindly update the url in the client's proxy.conf.json accordingly.



NOTE 2: For the client: I installed Aurelia Cli and created a new project.

I added bootstrap to the project after which the client did not serve successfully.

I checked online for possible solutions, however, I did not get any.

Reaching the conclusion that I might not be able to find the fix in time, 

considering the limited time given and rather than abandoning the project,

I decided to use Angular 9 for the client project. Since the general purpose is to see how I code (My thinking). 

Though i used another framwork, I ensured i followed the enumerated guidelines for the client app.


API
https://github.com/Hobinnah/HahnApplicantApp.git

Client
https://github.com/Hobinnah/HahnWebClient.git

The client currently uses the url http://localhost:5000 in the proxy.conf.json to connect to the WebAPI.
If this url changes, kindly update the proxy.conf.json accordingly.
