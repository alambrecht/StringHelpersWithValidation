# String Helper with Validation

A collection of string helpers and validation helpers

### Remove all non-numeric characters from a string
```
var numericString = "123ABC456.78".RemoveNonNumericCharacters();
returns "123456.78"
```

### Get Clean Guid
```
var cleanGuid = GetCleanGuid;
returns "B84E751DD25344C188B2C3ED0A21DF64"
```

### Get Email Addresses From String
Included delimiters are , ; or a space
```
var stringArray = "a@a.com;b@b.com;c@c.com".GetEmailsFromString();
```

### Format Phone Number
Takes a 7, 10, 11, or 12 digit phone number and formats it
```
var formattedNumber = "5558887777".FormatPhoneNumber();
```
Ex:
888777 returns 888-7777
555888777 returns (555) 888-7777
1555888777 returns +01 (555) 888-7777
01555888777 returns +01 (555) 888-7777

### Generate a List of Random Strings (No Duplicates)

Takes size (length of each string), quantity (Number of randoms strings to generate) and a string of characters used to randomize.
By default the following characters are used: "ABCDEFGHJKMNPQRSTUVWXYZ23456789"
```
var listOfRandomStrings = RandomString(5, 100, "ABCDEFGHJKMNPQRSTUVWXYZ23456789");
var listOfRandomStrings = RandomString(5, 100);
```

### Generate a Random String
```
var randomString = RandomString(5);
var randomString = RandomString(5, "ABCDEFGHJKMNPQRSTUVWXYZ23456789");
```

### Convert a String to a Byte Array
```
var byteArray = "ABC".ToByteArray();
```

### Validate an Email Address
```
var isValidEmail = "a@a.com".IsValidEmail();
```

### Validate HIPAA Compliant Password
Must be 8 characters long, contain 1 uppercase, 1 lowercase, 1 number and 1 special character
```
var isValidPassword = "ABc1234#".ValidateHipaaPassword();
```

### Split String Into Array Using Specified Delimiters
```
var stringArray = "A,B C,D E".SplitString(new{",", " "});
```