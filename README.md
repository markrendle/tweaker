Tweaker
=======

> An ASP.NET module to fix the horrible HTML that some things generate.

Seriously, some of these CMS systems and whatever else people keep using just make a total mess of even the easy stuff. Things like putting all your JavaScript at the end of the `body` element, or keeping all your CSS `link` elements together. Basic [PageSpeed](https://developers.google.com/speed/pagespeed/) 101 stuff, people. And of course you can't get in there and change the way these things are generating all this hideous HTML, because they're all black-boxy and inconfigurable (screw you, auto-correct, that is too a word).

### So what can you do?

What you can do is, you can wait until they've finished randomly spewing out HTML tags like a hyperactive toddler splatter-painting an internet, then do your best to fix it. Kind of like how they tried to fix the Star Wars prequels after George Lucas was done "directing" them, except hopefully with better end results.

Look, I'm waffling. It's on [NuGet](http://nuget.org/packages/Tweaker) so `install-package tweaker` in your web application, add it in the `<modules>` bit of `web.config`:

```
<system.webServer>
  <modules>
    <add name="Tweaker" type="Tweaker.DefaultTweaker"/>
  </modules>
</system.webServer>
```

 and it should just clean up any HTML that passes through.

Right now it just moves script and link tags around, but I've got some more advanced stuff I want to put in there, some image bits, maybe mess around with HTTP headers for caching and such, too. And of course you're going to fork it and send me PRs with your own awesome ideas, aren't you? Yeah you are. Check the code out, it's really easy to extend/modify/break.

Oh, BTW, if there's a script tag you really, really need to be in the `<head>` element, or exactly wherever you put it (things like [html5shiv](https://github.com/aFarkas/html5shiv) or funky asynchronous analytics loaders), you can add a `data-pin` attribute to it and it'll get left exactly where it is.

## Props

[CsQuery](https://github.com/jamietre/CsQuery) does literally all the hard stuff (and fast, too).