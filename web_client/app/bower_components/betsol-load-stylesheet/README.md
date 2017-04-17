# load-stylesheet

[![Bower version](https://badge.fury.io/bo/betsol-load-stylesheet.svg)](http://badge.fury.io/bo/betsol-load-stylesheet)
[![npm version](https://badge.fury.io/js/betsol-load-stylesheet.svg)](http://badge.fury.io/js/betsol-load-stylesheet)


> Loads stylesheets on demand with consistent callback support

This small module allows you to load CSS-resources
asynchronously inside the browser on demand from JavaScript
with consistent callbacks.

It works in all modern browsers as of *March 2016*.
If you have some compatibility issues — please [create an issue][new-issue].


## Installation

### Install library with Bower

`bower install --save betsol-load-stylesheet`


### Install library with NPM

`npm i --save betsol-load-stylesheet`


### Add library to your page

``` html
<script src="/node_modules/betsol-load-stylesheet/dist/betsol-load-stylesheet.js"></script>
```

You should use minified version (`betsol-load-stylesheet.min.js`) in production.


## Usage

Just call `loadStylesheet()` global function with URL and an optional callback:

```javascript
var linkElement = window.loadStylesheet('/css/foo.css', function () {
  alert('CSS file loaded!');
});
```

The function will return a reference to the `<link>` DOM element
for your convenience. You can track added links that way an remove them
if necessary in the future.

## Changelog

Please see the [changelog][changelog] for list of changes.


## Feedback

If you have found a bug or have another issue with the library —
please [create an issue][new-issue].

If you have a question regarding the library or it's integration with your project —
consider asking a question at [StackOverflow][so-ask] and sending me a
link via [E-Mail][email]. I will be glad to help.

Have any ideas or propositions? Feel free to contact me by [E-Mail][email].

Cheers!


## Developer guide

Fork, clone, create a feature branch, commit, create a PR.

Run:

- `npm i` to initialize the project
- `gulp build` to re-build the dist files
- `gulp demo` to run the demo server on port `1337`

Do not add dist files to the PR itself.
We will re-compile the module manually each time before releasing.


## Support

If you like this library consider to add star on [GitHub repository][repo-gh].

Thank you!


## License

The MIT License (MIT)

Copyright (c) 2016 Slava Fomin II, BETTER SOLUTIONS

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

  [changelog]: changelog.md
  [so-ask]:    http://stackoverflow.com/questions/ask?tags=javascript
  [email]:     mailto:s.fomin@betsol.ru
  [new-issue]: https://github.com/betsol/load-stylesheet/issues/new
  [repo-gh]:   https://github.com/betsol/load-stylesheet
