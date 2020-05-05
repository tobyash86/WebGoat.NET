# WebGoat.NETCore

## A next generation WebGoat example project showing OWASP TOP 10 vulnerabilities in a practical way.

This is a re-implementation of the original WebGoat project for .NET:
https://github.com/rapPayne/WebGoat.Net

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


## Known issues:

1. Used LocalDB format is compatible only with Windows OS, it needs to be replaced with SQLite
2. No support for Docker Linux containers (see pt. 1).
3. Newest OWASP Top 10 is not covered. Vulnerabilities needs to be added to the code base.
4. Educational documents/trainings for each OWASP Top 10 category are missing (old OWASP Top 10 is covered).

