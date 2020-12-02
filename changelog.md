# Changelog

## Version 0.1
### Added
- Added scripts to facilitate launching the WebGoat application.
- Added .NET 5 SDK as a minimum requirement to run WebGoat locally.
- Added UI enhancements to improve the look and feel of the application.
- Added examples of a valid credit card number.
- Added improved validation of credit card numbers provided by the user.

### Fixed
- Fixed the varying number of featured products to always be four.
- Fixed the cart to always clear on logout.
- Fixed the order of products to be displayed alphabetically.
- Fixed entry duplication in the cart which caused a runtime exception on checkout.
- Fixed the Docker run commands to remove a Docker container when the container is stopped.
- Fixed case sensitivity of the search bar.
- Fixed the About page to remove outdated information and a broken link.

### Removed
- Removed redundant compilation warnings when the `dotnet publish` command is used.
- Removed the ClickJacking example.
- Removed the duplicated "User Name" field on the Registration page.

## Preview Release

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
