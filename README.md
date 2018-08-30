* TODO: custom cat icon?
* TODO: test cache miss

# README #

## About

Content Author Tokens for Sitecore (CATS!) allows content authors to create, manage, and use basic token replacement in the most common Sitecore fields.

For example:

TODO: link to demo image(s)

* Current version: 1.0.0
* About and Download: [Content Author Tokens for Sitecore](http://www.brandonbruno.com/TODO)

## Features

* Simple, text-based token replacement for common Sitecore fields (single-line text, text, rich text, image, general link).
* Token management in the content tree (admin & multi-tenant).
* Configurable token format.
* Admin page: reset cache + sync tokens + view cache.
* Built-in cache.
* Button on ribbon to display usable tokens.
* COMING SOON: Optional compatibility with Glass Mapper (see below).

## Requirements

* .NET 4.6.1 or greater
* Sitecore 8.2.0 or greater

## Getting Started

#### 1. Installation ####

CATS is installed via a Sitecore package zip file. Install the package in your content management (CM) environment.

#### 2. Configuration ####

After installing CATS, open the following configuration file and be sure it is configured to your preferences:

```
\App_Config\Include\SitecoreSpark\CATS\SitecoreSpark.CATS.Settings.config
```

The following settings are available:

 * `SitecoreSpark.CATS.StartTag` - The opening tag for tokens (default is "{{")
 * `SitecoreSpark.CATS.EndTag` - The closing tag for tokens (default is "}}")
 * `SitecoreSpark.CATS.CacheSize` - Size of token cache (default is 10MB)
 * `SitecoreSpark.CATS.DefaultDatabase` - Default content database (default is "master"); change this if you don't edit content out of "master"

 **Note**: The `CacheSize` setting is particularly important. Tokens must be stored in cache to render quickly.

## Admin Page ##

`/sitecore/admin/TokenTools.aspx`

#### 3. Verify the Installation ####

## Upgrading

## Troubleshooting 

## Other Notes

* Tokens are case sensitive
* Native field validation to ensure tokens are unique

Tokens cache is rebuilt:

 * When the Sitecore initialization pipeline runs
 * During a site publish
 * Manually via the TokenTools.aspx admin page

## Upcoming Features

* TODO: button on ribbon to sync tokens?

## Contact the Author

For questions / comments / issues, contact me:
* Twitter: [@BrandonMBruno](https://www.twitter.com/BrandonMBruno) or [@SitecoreSpark](https://www.twitter.com/SitecoreSpark)
* Email: bmbruno [at] gmail [dot] com
 
## License

MIT License. See accompanying LICENSE.TXT file.