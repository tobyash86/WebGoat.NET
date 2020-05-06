# WebGoat.NETCore

## A next generation WebGoat example project showing OWASP TOP 10 vulnerabilities in a practical way.

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

TBD

## Known issues:

1. Used LocalDB format is compatible only with Windows OS, it needs to be replaced with SQLite
2. No support for Docker Linux containers (see pt. 1).
3. Newest OWASP Top 10 is not covered. Vulnerabilities needs to be added to the code base.
4. Educational documents/trainings for each OWASP Top 10 category are missing (old OWASP Top 10 is covered).

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
- added docker scripts




