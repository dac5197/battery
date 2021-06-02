# DACME Battery
A Todo single-page application built in Blazor

![Project Image](https://www.donald-chaney.com/media/images/projects/btry_pre.png)

> Battery is a to do web app developed in .NET Blazor Server. It is my first .NET project. The user creates to do items called Charges that are organized in a Battery object. The Charges are assigned a Category, Priority, and Status. The options for these fields are per Battery and can be changed when creating the Battery or any time after.

---

### Table of Contents

- [Description](#description)
- [References](#references)
- [License](#license)
- [Author Info](#author-info)

---

## Description

In the Battery app, the user creates to do items called Charges that are organized in a Battery object.  The Charges are assigned a Category, Priority, and Status.  The options for these fields are per Battery and can be changed when creating the Battery or any time after.  I added customer validators to enforce business logic around the values these fields can have (such as at least one Category per Battery must be set as the default).  

Charges are displayed in the Battery detail page in a lane layout as cards similar to a Kanban board or in a table layout.  Clicking on a Charge title will navigate to the Charge detail page where the Charge information is display and can be changed.  Also notes and history track all changes to a Charge field or user-entered work note.  From the related items section, child Charges can be created for sub-tasks to the main to do item.

Each Battery can also have Tags created for it and assigned to Charges.  They help organize the Charges and aid in searching.  The Tags unique to the Battery and cannot have any duplicate names (Tags for different Batteries can have the same name).

Authentication is handled by the ASP.NET Core Identity pages and Blazor authorization components.  Unauthorized users are redirected to either the login page or request access page depending if that user is logged in or not.

The back-end operations are handled by Entity Framework Core and SQL Server.  The Entity Framework ORM was similar to the Django ORM.  Some things were easier (such as tracking field changes) while others were not (such as handling many-to-many relationships).

#### Technologies

- .Net 5
- ASP
- Blazor Server
- Bootstrap
- HTML / CSS

[Back To The Top](#read-me-template)

---

## References
[Back To The Top](#read-me-template)

Tutorials and other videos I watched to learn .Net.  All of these videos were helpful in building this project.

### IAmTimCorey
[TimCo Retail Manager Course Introduction - A full start to finish course](https://www.youtube.com/watch?v=Xtt6mS0p2_c&list=PLLWMQd6PeGY0bEMxObA6dtYXuJOGfxSPx)

[Design Patterns and Principles](https://www.youtube.com/watch?v=dhnsegiPXoo&list=PLLWMQd6PeGY3ob0Ga6vn1czFZfW6e-FLr)

### Nick Chapsas
[9 "rules" for cleaner code | Object Calisthenics](https://www.youtube.com/watch?v=gyrSiY4SHxI)

[Deploying Server-Side Blazor in Azure with SignalR service | Blazor Tutorial 13](https://www.youtube.com/watch?v=qe9qANk8Ecw)

---

## License

GNU General Public License v3.0

Copyright (c) [2021] [Don Chaney]

[LICENSE](https://github.com/dac5197/battery/blob/master/LISCENSE.md)

[Back To The Top](#read-me-template)

---

## Author Info

- LindedIn - [Don Chaney](https://www.linkedin.com/in/donald-chaney)
- Website - [Don Chaney](#)

[Back To The Top](#read-me-template)
