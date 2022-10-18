# README

## About

Content Author Tokens for Sitecore (CATS!) allows content authors to create, manage, and use basic token replacement in common Sitecore content fields.

For example, use this token...

![A sample of a CATS token in Sitecore. The identifier "Phone Underscore Number" surrounded by curly braces.](https://www.brandonbruno.com/sections/development/images/cats/token_example_01.png)

... in most field types ...

![A CATS token being utilized in content.](https://www.brandonbruno.com/sections/development/images/cats/token_example_01a.png)

... to get this resulting content when the page is rendered:

![A rendered CATS token on a web page. A phone number with dashes.](https://www.brandonbruno.com/sections/development/images/cats/token_example_02.png)

* Current version: 2.0.0
* About and Download: [Content Author Tokens for Sitecore](http://www.brandonbruno.com/sections/development/cats.html)

## Maintenance Notice

As of 2022, this module will be updated and maintained for all 10.x versions of Sitecore XM/XP (likely 10.3 and 10.4). This module may not support future Sitecore composable CMS products.

**Sitecore XM Cloud**: because XM Cloud does not utilize content delivery (CD) servers, CATS is _not_ compatible with XM Cloud. Do not attempt to integrate it with your XM CLoud codebase.

## Features

* Simple, text-based token replacement for common Sitecore fields (single-line text, text, rich text, image, general link).
* Token management in the content tree (admin & multi-tenant).
* Configurable token format.
* Admin page: sync tokens, view cache, reset cache.
* Built-in cache.
* Button on Content Editor ribbon to display available tokens.

## Requirements

* .NET 4.6.1 or greater
* Sitecore 8.2.0 or greater (tested on 8.2.0, 9.1.1, 9.2, 10, 10.1)

## Getting Started

#### 1. Installation

CATS is installed via a Sitecore package zip file. Install the package in your content management (CM) environments and content delivery environments (CD).

#### 2. Configuration

After installing CATS, open the following configuration file and be sure it is configured to your preferences:

```
\App_Config\Include\SitecoreSpark\CATS\SitecoreSpark.CATS.Settings.config
```

The following settings are available:

 * `SitecoreSpark.CATS.StartTag` - The opening tag for tokens (default is "{{")
 * `SitecoreSpark.CATS.EndTag` - The closing tag for tokens (default is "}}")
 * `SitecoreSpark.CATS.CacheSize` - Size of token cache (default is 10MB)
 * `SitecoreSpark.CATS.DefaultDatabase` - Default content database (default is "master"); change this if you don't edit content in "master"
 * `SitecoreSpark.CATS.PublishedDatabase` - Default database for published content (default is "web"); change this if you don't publish to "web"

 **Note on CachSize**: The `CacheSize` setting is particularly important. Tokens must be stored in cache in order to render, so be sure not to zero this out.
 
 **Note on Start/End Tags:** Do not use the traditional .NET escape character "\\" in your token start/end tag definition.

 #### 3. Verify the Installation

 To verify the installation, ensure that you have the following item in your `master` database:

 `/sitecore/System/Modules/Content Author Tokens`

 You can also check `ShowConfig.aspx` to verify the following configuration patches:

 * `sitecore/pipelines/initialize/SitecoreSpark.CATS.Processors.Pipelines.Initialize.InitCATS`
 * `sitecore/pipelines/renderField/SitecoreSpark.CATS.Processors.Pipelines.Initialize.TokenReplacer`
 * `sitecore/events/event[publish:end] SitecoreSpark.CATS.Handlers.UpdateTokens, SitecoreSpark.CATS`
 * `sitecore/events/event[publish:end:remote] SitecoreSpark.CATS.Handlers.UpdateTokens, SitecoreSpark.CATS`
 * `sitecore/commands/cats:tokenlist`

## Getting Started ##

### Quick Start ###

1. Add a new Token item under `/sitecore/system/Modules/Content Author Tokens/Token Library`
2. Set the Pattern field (whatever you want content authors to use)
3. Set the Output field (whatever you want to replace the token with during page rendering)
4. Publish (to ensure tokens are in 'web' database and synced to cache)
5. Tokens are ready for use in content

### Token Libraries ###

A token library is a folder that stores tokens. Out of the box, CATS provides a default token library:

`/sitecore/System/Modules/Content Author Tokens/Token Library`

Any other libraries created next to or under this node will automatically be available for content authors.

If you require additional token libraries (for example, under the `Content` node in a multi-tenant environment):

1. Insert a new item based on the `Token Library` template
	* `/sitecore/templates/Content Author Tokens/Token Library - {BEEE1586-D37F-4637-9798-0DFFA496E0FB}`
2. On the `Content Author Tokens` module item, add each new library to the 'Additonal token libraries' field
	* This tells CATS where to find tokens
	* All libraries must be added here; CATS will not recursively load libraries-in-libraries

To view a list of available tokens, content authors can click "View Tokens" under the Presentation tab on the Content Editor ribbon.

### Syncing Tokens to Cache ###

For performance reasons, all tokens are stored in cache and loaded from cache during page rendering. Cache management is mostly hands-off, but here are a few things to keep in mind:

* Cache is automatically rebuilt/updated during the following events:
	* Sitecore initialization
	* Publish operations (specifically the `publish:end` and `publish:end:remote` events)

* Cache can be manually rebuilt/updated by using the administration tool. See the **Troubleshooting** section below for more information.

## Troubleshooting 

An admin page - `TokenTools.aspx` - allows you to view cache status, clear token cache, and rebuild the token cache from scratch.

`https://<your_site>/sitecore/admin/TokenTools.aspx`

When rebuilding token cache from this page, tokens must be published to the `web` database first.

## Other Notes

* Tokens are case sensitive.
* Tokens have built-in field validation to ensure Patterns are unique across the entire Sitecore instance.
* Info, warnings, and errors are written to standard Sitecore logs; all logs are prepended with "[CATS]".
* CATS adds a few extra tokens to cache for internal use; these tokens will NOT be accounted for on the `TokenTools.aspx` page.

## Contact the Author

For questions / comments / issues, contact me:
* Twitter: [@BrandonMBruno](https://www.twitter.com/BrandonMBruno)
* Email: bmbruno [at] gmail [dot] com
 
## License

MIT License. See accompanying LICENSE.TXT file.
