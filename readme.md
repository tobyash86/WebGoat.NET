# WebGoat.NETCore

## The next generation of the WebGoat example project to demonstrate OWASP TOP 10 vulnerabilities

This is a re-implementation of the original [WebGoat project for .NET](https://github.com/rappayne/WebGoat.NET).

This web application is a learning platform that attempts to teach about
common web security flaws. It contains generic security flaws that apply to
most web applications. It also contains lessons that specifically pertain to
the .NET framework. The exercises in this app are intended to teach about 
web security attacks and how developers can overcome them.

### WARNING: 
THIS WEB APPLICATION CONTAINS NUMEROUS SECURITY VULNERABILITIES 
WHICH WILL RENDER YOUR COMPUTER VERY INSECURE WHILE RUNNING! IT IS HIGHLY
RECOMMENDED TO COMPLETELY DISCONNECT YOUR COMPUTER FROM ALL NETWORKS WHILE
RUNNING!

### Notes:
 - Google Chrome performs filtering for reflected XSS attacks. These attacks
   will not work unless chrome is run with the argument 
   `--disable-xss-auditor`.

## How to build and run

### 1. Running in a Docker container

The provided Dockerfile is compatible with both Linux and Windows containers.  
To build a Docker image, execute the following command:

```sh
docker build --pull --rm -t webgoat .
```

#### Linux containers

To run the `webgoat` image, execute the following command:

```sh
docker run -d -p 5000:80 --name webgoat webgoat
```

WebGoat.NETCore website should be accessible at http://localhost:5000.

#### Windows containers

To run `webgoat` image, execute the following command:

```sh
docker run --name webgoat webgoat
```

Windows containers do not support binding to localhost. To access the website, you need to provide the IP address of your Docker container. To obtain the IP, execute the following command:

```sh
docker exec webgoat ipconfig
```
The output will include the IP of the 'webgoat' container, for example:

```
Ethernet adapter Ethernet:

   Connection-specific DNS Suffix  . : 
   Link-local IPv6 Address . . . . . : fe80::1967:6598:124:cfa3%4
   IPv4 Address. . . . . . . . . . . : 172.29.245.43
   Subnet Mask . . . . . . . . . . . : 255.255.240.0
   Default Gateway . . . . . . . . . : 172.29.240.1
```

In the above example, you can access the WebGoat.NETCore website at http://172.29.245.43.

### 2. Run locally using dotnet.exe (Kestrel)

1. Build and publish WebGoat.NETCore with the following command:

```sh
dotnet publish -c release -o ./app 
```

The web application will be deployed to the `app` folder in the current directory.

2. Execute the web application on localhost with the following command:

```sh
dotnet ./app/WebGoatCore.dll --urls=http://localhost:5000
```

The the WebGoat.NETCore website will be accessible at the URL specified with the `--urls` parameter: http://localhost:5000.


## Known issues:

1. The latest OWASP Top 10 is not covered. The uncovered vulnerabilities need to be added to the code base.
2. Educational documents/trainings for any categories of the latest OWASP Top 10 are not available (the previous version of OWASP Top 10 is covered).
3. The ClickJacking example is currently not functional.
4. There are some raw SQL queries in the code. We should consider using EF Core instead.
5. There is an exeption thrown after checkout if there are two exactly the same product entries in the cart.
6. Sometimes there is only one featured product diplayed on main page (instead of four).

## Changelog:

### Initial version:
- Converted WebGoat.NET (.NET Framework) to WebGoat.NETCore (.NET Core).
- Updated a set of functionalities to be compatible with .NET Core:
    - register/login/logout
    - cart/checkout
    - blog
    - products management
    - shipment tracking
- Improved the site styles.
- Added redirecting to the recent page after login.
- Included exception data on error pages.
- Improved spelling and formatting.
- Improved the build process.
- Fixed the 'Keep shopping' link.
- Improved the error messages for required form fields.
- Fixed the exception when no CCN was specified.
- Fixed order value calculation on checkout.
- Added support for running on Linux OS.
- Replaced SQL Server Local DB with SQLite database.
- Added support for running WebGoat in a Linux Docker container.
- Improved formatting of prices (rounding + removed $ character for consistency)




