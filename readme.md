# WebGoat.NET version 0.3

## Build status

![build .NET 8](https://github.com/tobyash86/WebGoat.NET/workflows/build%20.NET%208/badge.svg)

## The next generation of the WebGoat example project to demonstrate OWASP TOP 10 vulnerabilities

This is a re-implementation of the original [WebGoat project for .NET](https://github.com/rappayne/WebGoat.NET).

This web application is a learning platform that attempts to teach about
common web security flaws. It contains generic security flaws that apply to
most web applications. It also contains lessons that specifically pertain to
the .NET framework. The exercises in this app are intended to teach about 
web security attacks and how developers can overcome them.

### WARNING!: 
THIS WEB APPLICATION CONTAINS NUMEROUS SECURITY VULNERABILITIES 
WHICH WILL RENDER YOUR COMPUTER VERY INSECURE WHILE RUNNING! IT IS HIGHLY
RECOMMENDED TO COMPLETELY DISCONNECT YOUR COMPUTER FROM ALL NETWORKS WHILE
RUNNING!

### Notes:
 - Google Chrome performs filtering for reflected XSS attacks. These attacks
   will not work unless chrome is run with the argument 
   `--disable-xss-auditor`.

## Requirements
- .NET 8 SDK

## How to build and run

### 1. Running in a Docker container

The provided Dockerfile is compatible with both Linux and Windows containers.  
To build a Docker image, execute the following command:

```sh
docker build --pull --rm -t webgoat.net .
```

Please note that Linux image is already built by pipeline and can be pulled from [here](https://github.com/users/tobyash86/packages?repo_name=WebGoat.NET).

#### Linux containers

To run the `webgoat.net` image, execute the following command:

```sh
docker run --rm -d -p 5000:80 --name webgoat.net webgoat.net
```

WebGoat.NET website should be accessible at http://localhost:5000.

#### Windows containers

To run `webgoat.net` image, execute the following command:

```sh
docker run --rm --name webgoat.net webgoat.net
```

Windows containers do not support binding to localhost. To access the website, you need to provide the IP address of your Docker container. To obtain the IP, execute the following command:

```sh
docker exec webgoat.net ipconfig
```
The output will include the IP of the 'webgoat.net' container, for example:

```
Ethernet adapter Ethernet:

   Connection-specific DNS Suffix  . : 
   Link-local IPv6 Address . . . . . : fe80::1967:6598:124:cfa3%4
   IPv4 Address. . . . . . . . . . . : 172.29.245.43
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . : 172.29.240.1
```

In the above example, you can access the WebGoat.NETCore website at http://172.29.245.43.

#### Stopping Docker container

To stop the `webgoat.net` container, execute the following command:

```sh
docker stop webgoat.net
```

### 2. Run locally using dotnet.exe (Kestrel)

1. Build and publish WebGoat.NET with the following command:

```sh
dotnet publish -c release -o ./app 
```

The web application will be deployed to the `app` folder in the current directory.

2. Execute the web application on localhost with the following command:

```sh
dotnet ./app/WebGoat.NET.dll --urls=http://localhost:5000
```

The the WebGoat.NET website will be accessible at the URL specified with the `--urls` parameter: http://localhost:5000.

### 3. Run using a script
The WebGoat.NET projects ships with scripts that allow you to conveniently run the web application. The following scripts are located in the the "script" directory in the root of the project:
- runInDocker.bat - Runs the application in a Docker container on Windows.
- runInDocker.sh - Runs the application in a Docker container on Linux.
- runLocal.bat - Runs the application locally on Windows.
- runLocal.sh - Runs the application locally on Linux.

## Known issues:

1. The latest OWASP Top 10 is not covered. The uncovered vulnerabilities need to be added to the code base.
2. Educational documents/trainings for any categories of the latest OWASP Top 10 are not available.


