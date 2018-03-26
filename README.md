# Consolification



Consolification is a little framework to help you to write C# Console Applications, by managing argument parsing, help text generation and much more.

## The main features are:

- Automatically set class property values from given Console Application argument values (by associating Property and argument names via dedicated attributes). 
So, you don't have to parse given application arguments yourself.
- All .NET built in types supported.
- Argument format verification via Regex.
- Manages mandatory arguments and complex application argument structure, with parents and children concept: A child argument is an argument that is relevant only if another 'parent' argument is also specified in the Console Application command line. You can have several level of children arguments. Consolification manages that for you, by refusing, for example, a given child argument if a parent is not specified etc. 
- Automatically generate help text by using argument description provided within dedicated attributes.
- Encapsulate your code logic within a dedicated Job class, so you do not have to take care about Exception handling, help display (for example in case of wrong argument specified).


## Example for a quick start:

For a quick example, we will simply create a small Console Application that just display the text given in an argument.
As soon as you have create the Console Application (with a minimum .NET 4.6.2 version), add a dedicated class to handle data:
 
```C#
[CIHelpArgument("/?")]
[CIJob(typeof(MessageJob))]
public class Data
{
  [CISimpleArgument("message", "The message to display.")]
  public string Message { get; private set; }
}
```

The first attribute `CIHelpArgument`, on the class `Data`, specifies that you need to provide the argument '/?' to view the corresponding help.
The second class `Data` attribute `CIJob` indicates that the application logic is implemented within the class `MessageJob`.
The attribute `CISimpleArgument` on the property `Message` indicates that the Message property will receive the value of the first argument provided to the application. The other attribute parameters are dedicated to the automatically generated help text. We'll see that later.

Then, we'll have to create the job class that will display the message.

```C#
class MessageJob : IJob<Data>
{
    public int Run(JobContext<Data> context)
    {
        context.Console.WriteLine(context.Data.Message);
        return 0;
    }
}
```
The job implements the IJob generic interface from which you specify the class that handles arguments related data (`Data` in our small example).
From the `JobContext` instance, you have access to a `IConsoleWrapper`interface to interact with the console. The `Data` class instance, initialized with argumentsvalues given to the application, can be retrieved via the `JobContext<T>.Data` property.

Then, simply modify the Program class with the Main method like that:

```C#
class Program
{
  static int Main(string[] args)
  {
    ConsolificationEngine<Data> engine = new ConsolificationEngine<Data>();
    return engine.Start(args);
  }
}
```


Now, you just have to execute your application by giving a message as the single available argument (except the /? argument defined via the `CIHelpArgument` attribute).

For example, if your Console Application executable  is named Example.exe, juste type in the console:

```Batchfile
> Example "Hello the world!"
```

The result output will be simply 
```Batchfile
Hello the world!
```

Now, if you type: `> Example /?` the output will be the automatically generated help text:

```Batchfile
Usage: Example [/?] [message]

message   The message to display.
```

The brackets [] indicates optional arguments. If you specify that Message is a mandatory argument, by using the `CIMandatoryArgumentAttribute`, the help displayed will be:

```Batchfile
Usage: Example [/?] message

message The message to display.
```

The word 'message' to name the related argument has been specified in the `CISimpleArgumentAttribute` as second parameter. And the corresponding description ('The message to display') is taken from the last parameter of this same attribute.

Of course, for a so simple example, Consolification is not really usefull. But wait and see what will be the benefits when things are being more complex.


## Consolification Attribute list:

- [`CIArgumentBoundaryAttribute`](#CIArgumentBoundaryAttribute)
- [`CIArgumentFormatAttribute`](#CIArgumentFormatAttribute)
- [`CIArgumentValueLengthAttribute`](#CIArgumentValueLengthAttribute)
- [`CIChildArgumentAttribute`](#CIChildArgumentAttribute)
- [`CICommandDescriptionAttribute`](#CICommandDescriptionAttribute)
- [`CIFileContentAttribute`](#CIFileContentAttribute)
- [`CIHelpArgumentAttribute`](#CIHelpArgumentAttribute)
- [`CIJobAttribute`](#CIJobAttribute)
- [`CIMandatoryArgumentAttribute`](#CIMandatoryArgumentAttribute)
- [`CINamedArgumentAttribute`](#CINamedArgumentAttribute)
- [`CIParentArgumentAttribute`](#CIParentArgumentAttribute)
- [`CIPasswordAttribute`](#CIPasswordAttribute)
- [`CIShortcutArgumentAttribute`](#CIShortcutArgumentAttribute)
- [`CISimpleArgumentAttribute`](#CISimpleArgumentAttribute)


### CIArgumentBoundaryAttribute
Use this attribute to control the value of all argument for which the corresponding mapped type implements the `System.IComparable` interface, like all numerical value type (`System.Int32`, `System.Int64`, `System.Decimal` ...).
If the value of the correpsonding argument is lower or greater than values specified within this attribute, an error like the following  will be displayed:

```
ERROR while parsing arguments.
 The value of the argument '<ARG NAME>' cannot be greater than '<MAX VAL>'
```

### CIArgumentFormatAttribute
Use this attribute to control the format of an argument value, when this value is mapped to a string. 

### CIArgumentValueLengthAttribute
Use this attribute to control the length of an argument value, when this value is mapped to a string.

### CIChildArgumentAttribute

### CICommandDescriptionAttribute


### CIFileContentAttribute


### CIHelpArgumentAttribute



### CIJobAttribute


### CIMandatoryArgumentAttribute
Allows you to ensure that an argument is given to the Console Application. If not, a specific message is displayed to indicate the name of the missing argument and the help text is displayed.

Note that if your argument is also a child argument (See `CIChildArgumentAttribute`) and the corresponding parent argument is not mandatory and not specified in the command line, no error will be generated. The level of the argument is checked in the argument hierarchy to ensure that a mandatory argument error is generated only when needed!
For example, imagine your Console Application can connect a remote system. By default, you can use it without specifying any credential. But, optionaly, you can specify authentication parameters. So, you will have to define an argument to specify that you want for example a basic authentication. Let's say "-basicauthentication". For that, you can use the following property with its attribute:
```C#
[CIShortcutArgumentAttribute("-basicauthentication", "-ba")]
[CIParentArgument(0)]
public bool BasicAuthentication { get; private set }
```

When the property type is a boolean, there is no specific value to give next to the argument itself (like we have with the /URL previous argument). If the argument is specified in the command line, the corresponding boolean value is set to true. Otherwise, it is set to false. 

Then, you have of course to also add two properties to handle user name and password for your basic authentication. But, the arguments corresponding to those properties are required only if -basicauthentication is specified. So, you have to specify that `user` and `password` arguments are children arguments of the `-basicauthentication` argument:

```C#
[CIShortcutArgumentAttribute("-user", "-u")]
[CIChildArgument(0)]
[CIMandatoryArgument]
public string User { get; private set }

[CIShortcutArgumentAttribute("-password", "-p")]
[CIChildArgument(0)]
[CIMandatoryArgument]
public string Password { get; private set }
```

Now, `-user` and `-password` are mandatory only if `-basicauthentication` is specified. 



### CINamedArgumentAttribute
Use this attribute to define an argument that has got a specific name. Imagines for example you have a Console Application for which you can pass two arguments like `/URL http://www.google.fr`. In this case, use the `CINamesdArgumentAttribute` like that in your corresponding data class:

```C#
 [CINamedArgument("/URL", "The URL of the request to perform.", "valid url")]
 public Uri URL { get; private set; }
```

The URL property, from our previous example, will receive the value `http://www.google.fr` and of course, not `/URL` like with a CISimpleArgumentAttribute. 
When you display the correpsonding help, the following information will be displayed in the usage line:
`[URL <valid url>]`  ('valid url' comes from the last argument of `CINamedArgumentAttribute`.
And the /URL argument description line will look like:
`/URL   The URL of the request to perform.`

Notes that we have associated the `System.Uri` type to the URL property. But, we could have used the `System.String` type also. But with `System.Uri` type, a specific validation is performed automatically by the type constructor itself, so, it's better in this case, to ensure that user will specify a valid URI.

To view the complete list of supported types, see the section [Supported types](#consolification-supported-type-of-data-mapping)


### CIParentArgumentAttribute


### CIPasswordAttribute

### CISimpleArgumentAttribute
The attribute used in the example above. 

### CIShortcutArgumentAttribute
Similar to the `CINamedArgumentAttribute` except that you can also specify a shortcut name for your argument (for example, "/u" in addition to "/user" default argument name).






#### Consolification supported type of data mapping

