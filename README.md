# WizardGame

This is a write of the first project that I ever started building[^1], a text based RPG-Style game where you play a wizard that's out to get gold for... a reason...

This also comes with a snazzy Console library I wrote to use for this game, if you want to use that.. have fun.

## Contributing

I'm REALLY particular about my code style, there are some things that make me mad, personally. I don't expect *EVERYONE* to follow these rules, but if you do that would certainly be a plus on your PR.

Some quick steps before I get into my style:

1. Clone repo `git clone https://github.com/Gammer0909/WizardGame.git`
2. Make your changes
3. Build and make sure it all works, please D: (Idk how to set up a testing suite yet..)
4. Make a PR

Can't come up with anything to PR but wanna help? Here are some ideas:

1. Typo fixes! Those are always helpers!
2. A class, method, property, etc. missing XML Documentation[^2]? Add some!
3. Can you come up with a better way to do something? Fix it!

Ok, on to my coding style:

### My Coding Style

I use C#'s official naming rules for classes, properties, etc.

However, I do some things specifically:

```cs
// I use `var` instead of the `new()` syntax:
var obj = new Object();

// I use K&R indenting
void Method() {
  Something();
  if (true)
    SomethingElse();
}
```

If you use my code style, not only does it make me happy[^3], it limits tech debt a little.

## Testing

Honestly, I can't figure out how to setup a testing suite for my C# projects, any help with this would be much appreciated!

[^1]: The project was an idea that was given to me from the last episode of [Brackeys](https://www.youtube.com/channel/UCYbK_tjZ2OrIZFBvU6CCMiA)' [C# Tutorial](https://www.youtube.com/playlist?list=PLPV2KyIb3jR4CtEelGPsmPzlvP7ISPYzR)

[^2]: XML documentation makes using an API or framework easier. Here's a [reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)

[^3]: Only one happiness per contributor.
