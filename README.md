# Software architecture (R4.01)
__Author:__ Franco Nicolas           
__Group:__ PM2    
__Year:__ 2A BUT      

This project was developed as part of the R4.01 course "Software Architecture" taught by Mr. Chevaldonne, Ms. Millet, and Mr. Raymmond. This README file contains my documentation, it includes a detailed description of the project, its architecture, the technologies used, how they work, the initial goals, and the actual achievements.


Table of contents :page_with_curl:
=================

<!--ts-->
   * [Introduction](#installation)
   * [Usage](#usage)
      * [Installation](#installation)
      * [Execution](#execution)
   * [Structure](#structure)
      * [Description](#description)
   * [Entity Framework](#ef)
        * [Goals](#goals)
        * [What was done ?](#done)
   * [RESTful Api](#api)
        * [Goals](#goals)
        * [What was done ?](#done)
   * [CI/CD](#cicd)
   * [What's left ?](#next)


<!--te-->

## Structure
 ![Project's architecture diagram](./Documentation/architecture.png "Project's architecture diagram")

### Description
This project supports three different types of __clients__: <u>mobile</u>, <u>web</u>, and <u>desktop</u>. All of these clients communicate with a centralized __model__, which is responsible for handling <u>data</u> and <u>business</u> logic. 

Initially, the model uses fake data, as indicated by the green __Stub__ area. However, as the project progressed, the model was updated to use data from an API client. 

This client connects to a __Web API__, which serves as a bridge between the model and the data storage layer. The Web API interacts with an Entity Framework __(EF) database__, which is responsible for storing and managing data. In its early developement, the API would also use fake data, hence connection with the Stub. Both the model and the API interact with this database to retrieve and store data as needed. Additionnaly, the Web API is hosted on a __server__ [1], which allows clients to easily access it and interact with the model and data.

By using a central model and a Web API, we have designed a flexible architecture that can accommodate different types of clients and data storage systems.

[1]  _To be more precise, the Web API is deployed on a Docker container, the purpose of this representation is simply to convey the idea that the API is hosted on a separate system._
