# Consolification



Consolification is a little framework to help you to write C# Console Applications, by managing argument parsing, argument hierarchy, mandatory values, help text generation and much more.

## The main features are:

- Automatically set class property values from given Console Application argument values (by associating Property and argument names via dedicated attributes). 
So, you don't have to parse given application arguments yourself.
- All .NET built in types supported.
- Support of user types to create arguments hierarchy.
- Manages mandatory arguments and complex application argument structure, with parents and children concept: A child argument is an argument that is relevant only if another 'parent' argument is also specified in the Console Application command line. You can have several level of children arguments. Consolification manages that for you, by refusing, for example, a given child argument if a parent is not specified etc. 
- Automatically generate help text by using argument description provided within dedicated attributes.
- Encapsulate your code logic within a dedicated Job class, so you do not have to take care about Exception handling, help display (for example in case of wrong argument specified).
- Argument format verification via Regex.



## Example for a quick start:

For a quick example, we will simply create a small Console Application that just display the text given in an argument.
As soon as you have create the Console Application (with a minimum .NET 4.5 version), add a dedicated class to handle data:
 
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

The word 'message' to name the related argument has been specified in the `CISimpleArgumentAttribute` as first parameter. And the corresponding description ('The message to display') is taken from the last parameter of this same attribute.

Of course, for a so simple example, Consolification is not really usefull. But wait and see what will be the benefits when things are being more complex.

## User types and argument hierarchy

For advanced Console Applications, you often need a large bunch of arguments. Imagine for example you need to create a Console Application similar to the `NET` DOS command. This command provides several major features, to manage Windows network services, network shares, display network settings etc. All those major features are grouped behind arguments (ACCOUNTS, COMPUTER, CONFIG, START...). When you use one of those arguments, you can also specify other arguments dedicated to those 'parents' arguments (like /ADD for the COMPUTER parent argument). In Consolification library, '/ADD' is called a child argument. And COMPUTER is its parent. Meaning that, /ADD can only appear if you have use the COMPUTER argument.
To easily implement this argument hierarchy within the Consolification library, you have to create dedicated classes and embed those classes to your main data class as public properties.

### Creating user type

To implement something similar to the NET command, to stay with our previous example, we can create a data class that will look like the following example:
```C#
puclic class NetData
{
    [CINamedArgument("ACCOUNTS")]
    public AccountsData Accounts { get; private set; }
    
    [CINamedArgument("COMPUTER")]
    public ComputerData Computer { get; private set; }
    
    ...
}

public class AccountsData
{
    [CINamedArgument("/FORCELOGOFF")]
    public String ForceLogOff { get; private set;
    
    [CINamedArgument("/MINPWLEN")]
    public int MinPasswordLength { get; private set;
    
    ...
}

```

The NetData class exposes all commands that come just after the `NET` executable name. For each one, a dedicated type is created (like the AccountsData class) to embed all arguments related to the parent arguments.
When executed, if the `ACCOUNTS` argument is specified, the `NetData.Accounts` property won't be null.

Not that, in this case, you would like to avoid to have `ACCOUNTS` argument used in conjonction with `COMPUTER` argument (or with any other argument at this level). The [`CIExclusiveArgumentAttribute`](#ciexclusiveargumentattribute) attribute is dedicated to that purpose. So, your NetData class will look like that:

following example:
```C#
puclic class NetData
{
    [CINamedArgument("ACCOUNTS")]
    [CIExclusiveArgument]
    public AccountsData Accounts { get; private set; }
    
    [CINamedArgument("COMPUTER")]
    [CIExclusiveArgument]
    public ComputerData Computer { get; private set; }
    
    ...
}
```

Ok perfect. But wait, what happen if I do not provide any argument to the application? Depending of your corresponding IJob implementation, the application won't work as expected as arguments 'ACCOUNTS', 'COMPUTER'... are mandatories.
You cannot use the `CIMandatoryArgument` attribute for all those arguments because each one will be mandatory. It's not what we want. We want to that at least one argument among the list of available 'parent' arguments is presnet. To achieve that, the `CIGroupedMandatoryArgument` an be use:

```C#
puclic class NetData
{
    [CINamedArgument("ACCOUNTS")]
    [CIExclusiveArgument]
    [CIGroupedMandatoryArgument(1)]
    public AccountsData Accounts { get; private set; }
    
    [CINamedArgument("COMPUTER")]
    [CIExclusiveArgument]
    [CIGroupedMandatoryArgument(1)]
    public ComputerData Computer { get; private set; }
    
    ...
}
```

The `CIGroupedMandatoryArgument` attribute associated to each property indicates that we must have at least one argument specified among the list of arguments identified by the same group identifier.


## Consolification Attribute list:

- [`CIArgumentBoundaryAttribute`](#ciargumentboundaryattribute)
- [`CIArgumentFormatAttribute`](#ciargumentformatattribute)
- [`CIArgumentValueLengthAttribute`](#ciargumentvalueLengthattribute)
- [`CICommandDescriptionAttribute`](#cicommanddescriptionattribute)
- [`CIExclusiveArgumentAttribute`](#ciexclusiveargumentattribute)
- [`CIFileContentAttribute`](#cifilecontentattribute)
- [`CIGroupedMandatoryArgumentAttribute`](#cigroupedmandatoryargumentattribute)
- [`CIHelpArgumentAttribute`](#cihelpargumentattribute)
- [`CIJobAttribute`](#cijobattribute)
- [`CIMandatoryArgumentAttribute`](#cimandatoryargumentattribute)
- [`CINamedArgumentAttribute`](#cinamedargumentattribute)
- [`CIPasswordAttribute`](#cipasswordattribute)
- [`CIShortcutArgumentAttribute`](#cishortcutargumentattribute)
- [`CISimpleArgumentAttribute`](#cisimpleargumentattribute)


### CIArgumentBoundaryAttribute
:white_check_mark: This attribute can be applied to properties only.

Use this attribute to control the value of all argument for which the corresponding mapped type implements the `System.IComparable` interface, like all numerical value type (`System.Int32`, `System.Int64`, `System.Decimal` ...).

Usage example:
```C#
[CIArgumentBoundary("1", "86400")]
public int SecondsInDay { get; set; }

...

[CIArgumentBoundary("a", "z")]
public char MyLowerChar { get; set; }

```

If the value of the correpsonding argument is lower or greater than values specified within this attribute, an error like the following  will be displayed:

```
ERROR while parsing arguments.
 The value of the argument '<ARG NAME>' cannot be greater than '<MAX VAL>'
```

### CIArgumentFormatAttribute
:white_check_mark: This attribute can be applied to properties only.

Use this attribute to control the format of an argument value, when this value is mapped to a string. 
The first argument of this attribute is a regular expression in the form of a string.

Usage example:
```C#
 [CIArgumentFormat(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$")]
 public string EmailAddress { get; set; }

```

### CIArgumentValueLengthAttribute
:white_check_mark: This attribute can be applied to properties only.

Use this attribute to control the length of an argument value, when this value is mapped to a string.

Usage example:
```C#
[CIArgumentValueLength(12, 20)]
public string MyString1 { get; set; }
```
 

### CICommandDescriptionAttribute
:white_check_mark: This attribute can be applied to classes only.

Provides a description of the related command. This description is used within the auto generated help to provide a summary of what the related command does.

Usage example:
```C#
[CICommandDescription("Performs an HTTP request and get some result statistics.")]
public class RequestData
{
```

### CIExclusiveArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

Ensures that other arguments at the same level cannot be specified if the associated argument is itself specified in the command line.
By default, the GroupId property is set to 0, meaning that the associated argument is exclusive to all other arguments at the same level. If you specify a specific GroupId, it will be exclusive only for other argument for which a CIExclusiveArgumentAttribute attribute is specified with the same GroupId.

Usage example:
```C#
 [CINamedArgument("/D1")]
 [CIExclusiveArgument(1)]
 public int Data1 { get; private set; }

 [CINamedArgument("/D2")]
 [CIExclusiveArgument(1)]
 public int Data2 { get; private set; }

 [CINamedArgument("/D3")]
 [CIExclusiveArgument]
 public int Data3 { get; private set; }

 [CINamedArgument("/D4")]
 public int Data4 { get; private set; }
```

In this example, if /D1 is specified, you cannot also specify /D2. 
If you specify /D3, /D4 cannot be specified. But /D1 or /D2 cannot specified with /D4 (not the same group id).

### CIFileContentAttribute
:white_check_mark: This attribute can be applied to properties only.

Specifies that argument value is a path of a file for which content must be automatically loaded into the corresponding property value.

This attribute can be associated with the following property types:
     1) `System.String`: The entire file content is put into a string.
     2) `System.String[]`: Each file line (only relevant for text file) is a string of the resulting array.
     3) `System.Byte[]`: The entire file content is put into a byte array.
     4) `System.Char[]`: The entire file content is put into a char array.
     5) `System.IO.FileStream`: An FileStream stream is opened from the corresponding file.

Usage example:
```C#
 [CINamedArgument("/FILE")]
 [CIFileContent]
 public byte[] FileByteArray { get; set; }

 [CINamedArgument("/FILELINES")]
 [CIFileContent]
 public string[] FileLines { get; set; }

 [CINamedArgument("/FILESTRING")]
 [CIFileContent("UTF8")]
 public string FileString { get; set; }

 [CINamedArgument("/FILECHAR")]
 [CIFileContent]
 public char[] FileCharArray { get; set; }

 [CINamedArgument("/FILESTREAM")]
 [CIFileContent]
 public FileStream FileStream { get; set; }
```

### CIGroupedMandatoryArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

Like the `CIMandatoryArgumentAttribute`, it specifies that the related argument is mandatory. But, unlike 
this attribute, it is associated to a "group" (see constructor group identifier parameter) that tells that only 
one argument associated with this group is mandatory.

Usage example:
```C#
  [CINamedArgument("-ARG1")]
  [CIGroupedMandatoryArgumentAttribute(1)]
  public int Arg1 { get; private set; }

  [CINamedArgument("-ARG2")]
  [CIGroupedMandatoryArgumentAttribute(1)]
  public int Arg2 { get; private set; }

  [CINamedArgument("-ARG3")]
  [CIMandatoryArgumentAttribute]
  public int Arg3 { get; private set; }
```

In this example, if -ARG3 is not specifeid, an error code is returned by the ConsolificationEngine. Same thing if
-ARG1 and -ARG2 are not specified. But if -ARG1 or/and -ARG2 are specified, it's ok. You are not obliged to specify both -ARG1 and -ARG2.

### CIHelpArgumentAttribute
:white_check_mark: This attribute can be applied to classes only.

Specifies the name of the argument used to display the command help.

Usage example:
```C#
[CIHelpArgument("/?")]
public class Data
{
 ...
```

From this example, you have to type your command executable name plus the argument "/?" to display the associated help file.

### CIJobAttribute
:white_check_mark: This attribute can be applied to classes only.

 Specifies which Job class will be instantiated and executed when the ConsolificationEngine.Start() method is called.
 You should put all your command logic within a Job.
 
 Usage example:
```C#
 [CIJob(typeof(RequestJob))]
 public class RequestData
 {
 ...
```

The job associated with this data class, RequestJob, can be defined like that:

```C#
public class RequestJob : IJob<RequestData>
{
        public int Run(JobContext<RequestData> context)
        {
            RequestData data = context.Data;         
            try
            {       
				          // Your execution logic here
            }
            catch (Exception exp)
            {
                context.Console.WriteLine("ERROR: Failed to execute HTTP request.", exp);
                return -1;
            }
        }
```


### CIMandatoryArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

Allows you to ensure that an argument is given to the Console Application. If not, a specific message is displayed to indicate the name of the missing argument and the help text is displayed.
												usertypesandargumenthierarchy
Note that if your argument is also a child argument (See [`User types and argument hierarchy`](#user-types-and-argument-hierarchy) ) and the corresponding parent argument is not mandatory and not specified in the command line, no error will be generated. The level of the argument is checked in the argument hierarchy to ensure that a mandatory argument error is generated only when needed!
For example, imagine your Console Application can connect a remote system. By default, you can use it without specifying any credential. But, optionaly, you can specify authentication parameters. So, you will have to define an argument to specify that you want for example a basic authentication. Let's say "-basicauthentication". For that, you can use the following property with its attribute:
```C#
[CIShortcutArgumentAttribute("-basicauthentication", "-ba")]
[CIParentArgument(0)]
public bool BasicAuthentication { get; private set }
```

When the property type is a boolean, there is no specific value to give next to the argument itself (like we have with the /URL previous argument). If the argument is specified in the command line, the corresponding boolean value is set to true. Otherwise, it is set to false. 

Then, you have of course to also add two properties to handle user name and password for your basic authentication. But, the arguments corresponding to those properties are required only if -basicauthentication is specified. So, you have to specify that `user` and `password` arguments are children arguments of the `/basicauth` (or `-b`) argument:

```C#
    [CIHelpArgument("/?")]
    [CICommandDescription("Performs an HTTP request and get some result statistics.")]
    [CIJob(typeof(RequestJob))]
    public class RequestData
    {
        ...

        [CIShortcutArgument("/basicauth", "/b", "Use BASIC authentication.")]
        public Credentials BasicAuthentication { get; private set; }
    }
    
    public class Credentials
    {
        [CIShortcutArgument("/user", "/u", "The user to authenticate the request.", "value")]
        [CIMandatoryArgument(true, "User name: ")]
        public string User { get; private set; }

        [CIShortcutArgument("/password", "/p", "This user password to authenticate the request.", "value")]
        [CIMandatoryArgument(true, "User password: ")]
        [CIPassword]
        public string Password { get; private set; }
    }
```

Now, `/user` and `/password` are mandatory only if `/basicauth` is specified (as the `Credentials` type is used for the `BasicAuthentication`property within the `RequestData` main class data.

Note there is also 2 optionals parameters given to the `CIMandatoryArgument` attributes. If those parameters are not specified, an error is generated if the corresponding argument is not specified when required. But, if you specify those arguments like in the example above, and if the corresponding argument is not specified, the value of this argument will be prompted in the console, by using the specified message (second parameter).


### CINamedArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

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


### CIPasswordAttribute
:white_check_mark: This attribute can be applied to properties only.

Specifies the corresponding argument value is a password. In that case, if the user must be prompted to enter the value, all characters read will be replaced by a specific character ('*' by default) in the console display.

 Usage example:
```C#
 [CIShortcutArgument("/password", "/p", "This user password to authenticate the request.", "value")]
 [CIMandatoryArgument(true, "User password: ")]
 [CIPassword]
 public string Password { get; private set; }
```

### CIShortcutArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

Similar to the `CINamedArgumentAttribute` except that you can also specify a shortcut name for your argument (for example, "/u" in addition to "/user" default argument name).

 Usage example:
```C#
 [CIShortcutArgument("/user", "/u")]
 public string UserName { get; private set; }
```

### CISimpleArgumentAttribute
:white_check_mark: This attribute can be applied to properties only.

Defines a simple argument (meaning that there is no name associated with this argument.
Think about the 'SOURCE' and 'DESTINATION' arguments in the DOS 'COPY' command. 

 Usage example:
```C#
 [CISimpleArgument("source", "Specifies the file or files to be copied.")]
 [CIMandatoryArgument]
 public string Source { get; private set; }
```


