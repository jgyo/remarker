# Remarker

Remarker is a Visual Studio extension designed to help programmers add remarks (comments) in their code that are easily visible and useful. Remarker uses color, boldface, typefaces and font sizes to highlight various types of remarks in the code.

This extension is primarily written for C#, but it is still able to work in any language supported by Visual Studio as long as comments start with "//" or exist on otherwise empty lines between multi-line comment delimiters. Visual Basic comments starting with an apostrophe are also supported. This partly depends on the way Visual Studio identifies and tags comments within its editors, so there may be cases where this does not work as expected.

In C# comments are modified by following the double slash comment delimiter with with optional modifier and and emphasis symbols. The modifiers are "!" (for important comments), "?" (for questions), and "x" (to indicate lines to strike out). The emphasis symbols are "-" (for less emphasis) and "+" (for more emphasis). Only one modifier may be used per line, and up to three of either emphasis symbols may be used. Modifier and emphasis symbols must come first within a comment after optional white space, but there must be no white space among the modifier and emphasis symbols. However, the modifier and emphasis symbols must be followed by white space to be recognized.

Note: Because SQL comments start with "--", previous versions of Remarker would present SQL comments as a "Tiny." comment, which is almost unreadable by default. For that reason Remarker is now disabled in SQL scripts.

Another feature of Remarker is its support for configurable headers called "tasks." If a task is the first word on a comment line, it receives special handling and can be given its own typeface, bold style, and color. Several tasks are defined, but these can be modified in any way you wish. Up to ten tasks can be defined.

The extension creates a Remarker section within Visual Studio's Options dialog box with two sub-pages defined. Comment Settings provides a place to modify the font used for comments, and size factors for each of the six emphasis settings. The Task Settings page provides a place to configure the tasks you want Remarker to recognize.

You will also find several Remarker entries within the Environment/Fonts and Colors page to configure the colors for the various emphasis levels.

There are several other extensions available to work similarly to Remarker. I have tried most, and all do a good job. The main difference between the others and Remarker is the fact that mine allows for spaces between comment delimiters and the extension's modifiers. This is important especially in Xaml when formatting tools commonly force white space there, breaking any emphasis given by such tools.

We hope you enjoy Remarker, and that you will let us know if you have any trouble with it. We value your suggestions and other comments.
