# WebGoat.NETCore

## A next generation WebGoat example project showing OWASP TOP 10 vulnerabilities in a practical way

This is a re-implementation of the original [WebGoat project for .NET](https://github.com/jerryhoff/WebGoat.NET).

This web application is a learning platform that attempts to teach about
common web security flaws. It contains generic security flaws that apply to
most web applications. It also contains lessons that specifically pertain to
the .NET framework. The excercises in this app are intented to teach about 
web security attacks and how developers can overcome them.

### WARNING: 
THIS WEB APPLICATION CONTAINS NUMEROUS SECURITY VULNERABILITIES 
WHICH WILL RENDER YOUR COMPUTER VERY INSECURURE WHILE RUNNING! IT IS HIGHLY
RECOMMENDED TO COMPLETELY DISCONNECT YOUR COMPUTER FROM ALL NETWORKS WHILE
RUNNING!

### Notes:
 - Google Chrome performs filtering for reflected XSS attacks. These attacks
   will not work unless chrome is run with the argument 
   --disable-xss-auditor. 
- Some (but not all!) of the lessons require a working SQL database. Setup
  guidelines are shown below.

## How to build and run?

### 1. Run in a Docker container

Provided Dockerfile is compatible with both Linux and Windows containers.  
To build a Docker image please execute command:

```sh
docker build --pull --rm -t webgoat .
```

#### Linux containers

To run `webgoat` image please execute command:

```sh
docker run -d -p 5000:80 --name webgoat webgoat
```

`WebGoat.NETCore` website should be accessible under address http://localhost:5000.

#### Windows containers

To run `webgoat` image please execute command:

```sh
docker run --name webgoat webgoat
```

Windows containers do not support binding to a localhost, so to access the website it is required to get Docker container IP adsress. This can be done by executing the following command:

```sh
docker exec webgoat ipconfig
```
as a result, output from ipconfig should appear, e.g:

```
Ethernet adapter Ethernet:

   Connection-specific DNS Suffix  . : 
   Link-local IPv6 Address . . . . . : fe80::1967:6598:124:cfa3%4
   IPv4 Address. . . . . . . . . . . : 172.29.245.43
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . : 172.29.240.1
```
Now it is clear that `WebGoat.NETCore` website should be acessible under e.g. http://172.29.245.43.

### 2. Run locally using dotnet.exe (Kestrel)

To run WebGoat.NETCore locally, first it needs to be built and published:

```sh
dotnet publish -c release -o ./app 
```

as a result built web application will be deployed into `app` folder in current directory. The following command will execute it on local host:

```sh
dotnet ./app/WebGoatCore.dll --urls=http://localhost:5000
```
As specified in `--urls` parameter, the web application will be hosted under http://localhost:5000.

## Known issues:

1. Newest OWASP Top 10 is not covered. Vulnerabilities needs to be added to the code base.
2. Educational documents/trainings for each OWASP Top 10 category are missing (old OWASP Top 10 is covered).
3. ClickJacking example is not functional at the moment.

## Changelog:

### Initial version:
- converted WebGoat.NET (.NET Framework) to WebGoat.NETCore (.NET Core)
- updated a set of functionalities to be compatible with .NET Core:
    - register/login/logout
    - cart/checkout
    - blog
    - products management
    - shipment tracking
- improved site styles
- redirecting to a recent page after login
- included exception data on error pages
- improvements around misspellings and formattings
- improved build experience
- fixed 'Keep shopping' link
- improved error messages for required form fields
- fixed exception when no CCN was specified
- fixed order value calculation on checkout
- added support for running on Linux OS
- replaced SQL Server Local DB with SQLite database
- added support for running WebGoat in Docker Linux container




