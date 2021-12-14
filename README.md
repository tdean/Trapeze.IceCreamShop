# Trapeze Ice Cream Store #

### Goal
As a store owner, I would like an API that allows me to keep track of my ice cream purchases easily.

The API will need to have 1 endpoint:

 - An endpoint to insert a record of what ice cream was purchased, including the name of the person who purchased the ice cream, the amount paid for the ice cream, the base of the ice cream, and the scoops and flavours of ice cream.

The general information about purchasing an ice cream are:

 - An ice cream can have any base out of the available base types. All bases cost $3, except for the Waffle Cone, which costs $4.
 - An ice cream can have one, two, three, or four scoops. The prices are $2, $3, $3.50, and $3.80 respectively.
 - The total cost of an ice cream cone is the cost of the base + the cost of the scoop(s).

Some validation rules when purchasing or getting the price of an ice cream:

 - The amount passed in to purchase must match or be greater than the price. If the amount is greater than the purchase price, the database should only record the purchase price, and the rest of the money must be refunded through the API (the API returns with a record specifying how much the refund amount is).
 - The ice cream base must be from the list of available bases.
 - The ice cream flavours must be from the list of available flavours.
 - Only a cup of ice cream can have 4 scoops.
 - We will not give Cookie Dough flavour in the Sugar Cone base.
 - We will not give Strawberry and Mint Chocolate Chip flavours together.
 - We will not give Cookies And Cream, Moose Tracks, and Vanilla together. We will give any combination of two of the above though. I.e. Cookies And Cream  and Vanilla are ok together.
	 - The only exception to the 3 combined ice cream flavours rule above is if the base is a Cake Cone.

### Development notes

 - The `IceCreamBase` enum and the `IceCreamFlavour` enum contain the base type and flavours mentioned above, and are found in the `Trapeze.IceCreamShop` project.
 - You **cannot** make any changes to the `IceCreamBase` or `IceCreamFlavour` enums.
 - The base data access layer has been provided using EntityFramework Core and the DbContext is called `IceCreamDbContext`. The DbContext is accessible through DI.
 - The database being used is a file database. The inserted data is saved to the file icecream.db found in the `Trapeze.IceCreamShop.Api` project. To delete the existing data, just delete this file.
 - The `Id`, `ParentId`, and  `PurchaseDateTime` fields are automatically populated by EntityFramework Core. You do **not** need to populate these fields yourself, or worry about them.
 - You shouldn't need to change anything in the `Trapeze.IceCreamShop.Data` project, but if you feel like it is necessary, you are free to make changes.
 - The service layer project `Trapeze.IceCreamShop.Services` has already been added, and the goal is to extend the existing service (and the underlying abstraction) to meet the needs of the API. The abstraction `IIceCreamShopService` is accessible through DI.
 - The endpoints for the API **must** be in the `IceCreamStoreController` found in the `Trapeze.IceCreamShop.Api` project.
 - The `PurchaserName` **must** be retrieved from the Identity on the `HttpContext` User.

### How do I get set up?

 - Using a service that opens .sln files (such as Visual Studio Code or Visual Studio), open the `Trapeze.IceCreamShop.sln` file. We recommend Visual Studio 2019.
 - The only configuration necessary to have it run is the connection string, which is already included in the appsettings.json file.

### How do I use the API?

The API has Basic authentication already rolled into the code. This means, an `Authorization` header is required when accessing the API. We recommend using Postman when calling the API to be able to easily set the `Authorization` header.

There are 3 users within the service:

 - Alanna Mosvani
	 - Username: `amosvani`
	 - Password: `TW9zdmFuaXh4`
	 - Authorization Header: `YW1vc3Zhbmk6VFc5emRtRnVhWGg0`
 -  Mat Cauthon
	 - Username: `mcauthon`
	 - Password: `Q2F1dGhvbnh4`
	 - Authorization Header: `bWNhdXRob246UTJGMWRHaHZibmg0`
 -  Moiraine Damodred
	 - Username: `mdamodred`
	 - Password: `RGFtb2RyZWR4`
	 - Authorization Header: `bWRhbW9kcmVkOlJHRnRiMlJ5WldSNA==`

To authorize, pass in an `Authorization` header with the format `Basic {header}` where the header is one of the 3 authorization headers above. I.e. `Basic YW1vc3Zhbmk6VFc5emRtRnVhWGg0`. Please note the space between `Basic` and the authorization header.

To do this in Postman, you have 2 methods. Either manually add the Authorization header to the list of headers, or follow the steps [here](https://learning.postman.com/docs/sending-requests/authorization/#basic-auth) to add the username and password combination.