## Database Systems - Overview
### _Homework_

#### Answer following questions in Markdown format (`.md` file)

1.  What database models do you know?
    * Hierarchical (tree)
    * Network / graph
    * Relational (table)
    * Object-oriented
1.  Which are the main functions performed by a Relational Database Management System (RDBMS)?
    * Relational Database Management Systems (RDBMS) manage the organization, storage, access, security and integrity of data.
1.  Define what is "table" in database terms.
    * A table is a set of data elements (values) using a model of vertical columns (identifiable by name) and horizontal rows, the cell being the unit where a row and column intersect. A table has a specified number of columns, but can have any number of rows.
1.  Explain the difference between a primary and a foreign key.
    * In a foreign key reference, a link is created between two tables when the column or columns that hold the primary key value for one table are referenced by the column or columns in another table. This column becomes a foreign key in the second table.
1.  Explain the different kinds of relationships between tables in relational databases.
    * Relationship one-to-many (or many-to-one)
      * A single record in the first table has many corresponding records in the second table
    * Relationship many-to-many
      * Records in the first table have many correspon-ding records in the second one and vice versa
      * Implemented through additional table
1.  When is a certain database schema normalized?
    * When there is no repeating data.
1.  What are the advantages of normalized databases?
    * Normalized databases need less memory.
1.  What are database integrity constraints and when are they used?
    * A way to ensure accuracy and consistency of the data.
    * When the data should follow some rules.
1.  Point out the pros and cons of using indexes in a database.
    * Indices speed up searching of values in a certain column or group of columns
    * Adding and deleting records in indexed tables is slower!
1.  What's the main purpose of the SQL language?
    * Manipulation of relational databases.
1.  What are transactions used for? Give an example.
    * Transactions are a sequence of database operations which are executed as a single unit:
      * Either all of them execute successfully
      * Or none of them is executed at all
    * Example:
      * A bank transfer from one account into another (withdrawal + deposit)
      * If either the withdrawal or the deposit fails the entire operation should be cancelled.
1.  What is a NoSQL database?
    * Non-relational database.
1.  Explain the classical non-relational data models.
    * Document model (e.g. MongoDB, CouchDB)
      * Set of documents, e.g. JSON strings
    * Key-value model (e.g. Redis)
      * Set of key-value pairs
    * Hierarchical key-value
      * Hierarchy of key-value pairs
    * Wide-column model (e.g. Cassandra)
      * Key-value model with schema
    * Object model (e.g. Cache)
      * Set of OOP-style objects
1.  Give few examples of NoSQL databases and their pros and cons.
    * Redis
      * Ultra-fast in-memory data structures server
    * MongoDB
      * Mature and powerful JSON-document database
    * CouchDB
      * JSON-based document database with REST API
    * Cassandra
      * Distributed wide-column database