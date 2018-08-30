# README #



## About

* Tokens are case sensitive.

## Features

* Simple, text-based token replacement for common Sitecore fields (single-line text, text, rich text, image, general link)
* Token management in content tree (admin-only & multi-tenant)
* Configurable token format
* Admin page: reset cache + sync tokens + view cache
* Built-in cache
* Validation to confirm that tokens are unique
* Button on ribbon to display usable tokens
* TODO: Optional compatibility with Glass Mapper

* TOOD: make sure no exceptions crash Sitecore INIT or PUBLISH processes - just write to logs
* TODO: make tokens not case sensitive
* TODO: use single char for token delimiter instead of strings? (performance reasons)

* REFERENCE: https://sitecorerunner.com/2018/08/21/adding-rte-richtext-custom-dropdown-list-in-sitecore-9/

## Requirements

* .NET 4.6.1 or greater
* Sitecore 8.2.0 or greater

## Getting Started

#### 1. Installation ####

#### 2. Configuration ####

#### 3. Verify the Installation ####

## Upgrading

## Troubleshooting 

## Other Notes

* Tokens are case sensitive?

Tokens cache is rebuilt:

 * When the Sitecore initialization pipeline runs
 * During a site publish
 * Manually via the TokenTools.aspx admin page

## Dependency Injection
  
## Upcoming Features

* TODO: button on ribbon to sync tokens?

## Contact the Author

For questions / comments / issues, contact me:
* Twitter: [@BrandonMBruno](https://www.twitter.com/BrandonMBruno) or [@SitecoreSpark](https://www.twitter.com/SitecoreSpark)
* Email: bmbruno [at] gmail [dot] com
 
## License

MIT License. See accompanying LICENSE.TXT file.