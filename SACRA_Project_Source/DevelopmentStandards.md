# Project Development Standards

## 1. Oracle SQL and PL/SQL Standards
## 1.1 Developer Responsibiities 
Take Ownership
Developers are expected to:

Produce SQL and PL/SQL code to the agreed standards, undertake unit testing and ensure the code performs in a timely fashion
Leave an audit trail, by referencing the origin of the change (defect, requirement, change request, etc.) and providing the reasoning for the solution at the time
Identify and raise outstanding DDL dependencies, such as new or altered tables, columns, constraints etc.
Delivered SQL and PL/SQL
All delivered SQL and PL/SQL code objects will:

Meet all specified business requirements
Include the business logic
Adhere to the SQL and PL/SQL coding standards
Have been through a peer review
Compile without errors
Have been performance tuned where they are expected to be run more than once
Acceptance
All delivered SQL or PL/SQL code must be reviewed and accepted by a Department of Corrections Data Architect before it can be deployed into production. Any code that does not meet these standards will not be accepted.
## 1.2 SQL and PL/SQL coding standards
#  Keep It Simple
Some of the best code, from a performance, readability and maintainability perspective is code that uses the design principle of ‘Keep It Simple’.

#  Style Consistency
Code style consists of many things, such as formatting, indenting, commenting, etc. When changing a procedure or function, stick to the style previously used as this provides consistency. If the code is not consistent then reformat the existing code. 

#  Peer Review
Make sure all code is peer reviewed. If the code is readable and commented, the peer review process will be easier, and the reviewers will be able to concentrate on the functionality of the code rather than on deciphering convoluted PL/SQL. Peer reviews provide a first line of defence against potential bugs. Appendix A is an example of a peer review checklist.

#  Modularity of Code
Always keep functionality modular. If you are writing long or complicated scripts, try to break the code into functional modules. Replace repeated code with a function/procedure, this makes code easier to maintain, as functionality only has to be changed in one place. Group procedures/functions for each subject area by implementing them in a single package. This has the benefit of:

-  Does not break the dependency chain. There will be no cascading invalidations when you install a new package body. If you have procedures that call procedures -- compiling one will invalidate your database)
-  Supports encapsulation. You can write modular, easy to understand code, rather than monolithic, non-understandable procedures
-  Increases namespace measurably. Package names must be unique within a schema, but you can have procedures across packages with the same name
-  Supports overloading
-  Supports session variables when needed

All tables must have their own insert, update and delete procedures. Do not use the existing package PKG_XML_CUD to perform CUD operations. The only exceptions to this are legacy IOMS services.

#  Code Comments

- Comments are one of the most important features of programming as they convey to the reader an understanding of the functionality which enhances the ability to debug, performance tune, and maintain code. 
- Keep comments brief and to the point, use them to explain the overall objective and functionality, or any complex and unusual code.
- Keep comments up to date.

## Header Comments

Always have a header comment that explains what the code is meant to do, and a brief explanation of the purpose of each parameter being passed. The header comment should include:
  -  Object name
  -  Project and references e.g. service requests, design documents, defects
  -  Purpose and comments, including the functionality being met
  -  Screen(s) it relates to
  -  Created by and date
  -  Modification history
      -  Details of change
      -  Modified by and date 

For example:


```
CREATE OR REPLACE PACKAGE BODY <pkg_package_name> IS
  --begin_property_map
  --scriptid=pkg_ package _name.sql
  --type=package
  --name=pkg_ package _name
  --order=1
  --release=yes
  --end_property_map
/***************************************************************
    Name:         pkg_package_name (package body)
    Project:      Example Project
    Design Ref:   Example – Design Doc V1.0 (TRIM Ref D99-9) 
    Purpose:      Overnight batch process for synchronisation of 
                  Example data between databases A and B
    Comments:     ro_procedure_name starts the overnight
    Screen(s):    Example Menu>Example Screen             
    Created By:   Joe Bloggs
    Created Date: 06/05/2014
    ------------------------------------------------------------
    Modification History
    ------------------------------------------------------------
    When        Who        Specification Change/TFS Bug   
                            No/Details of Change
    ----------  ---------  -------------------------------------       
    13/12/2014  Revathi    Example – Design Doc V1.2 (TRIM 
                            Ref D99-9) - Modified the script as 
                            per latest design document.
****************************************************************/
```

When a procedure, function or package changes, only add the modification history to the place that is changing e.g. if the changes only affect the package body then only include it within the package body not the package specification. If both specification and body change then include version change history in both. 

## SELECT Statement Comments

SELECT statements must contain the procedure/function name as a comment. This allows the identification of specific blocks of SQL code being executed on the server e.g.

```
SELECT /*+ fn_function_name */
SELECT /*+ ro_procedure_name */
```
It’s important to include the ‘+’ sign after the first asterisk. Within a UNION, each SELECT statement must contain the procedure/function name e.g. 


```
SELECT /*+<ro_procedure_name>*/ <column list>
UNION ALL                                                                                                              
SELECT /*+<ro_procedure_name>*/ <column list>
```

##  Code Readability
###	Spacing

-  Break up your code with white space by putting blank lines before and after blocks of code
-  Format complex statements over multiple lines
-  Always include a space between an identifier and an operator 
-  Use at most one statement per line
-  Place each table/join in a FROM clause on to its own line
-  Place each expression in WHERE clause on to its own line 
-  Use no more than one blank line within code
-  Use two blank lines between subprograms in a package or type body


```
CREATE OR REPLACE PROCEDURE ro_example
(
   pi_from_date IN DATE,
   po_cursor   OUT SYS_REFCURSOR
)
IS
   l_sysdate DATE := SYSDATE;
BEGIN
   OPEN po_cursor FOR
        SELECT et.example_date,
               tt.offender_id
          FROM example_table et 
               INNER JOIN test tt ON et.example_id = tt.example_id
         WHERE et.example_from_date = pi_from_date
           AND tt.test_date <> l_sysdate
      ORDER BY et.example_date DESC;
END ro_example;
/
```

### Lining Up

Line up long lists of repetitive code into a single column e.g. parameters, variables, select statements etc. 


```
CREATE OR REPLACE PROCEDURE ro_example
(
   pi_from_date IN DATE,
   pi_to_date IN DATE,
   pi_offender_id IN offender.offender_id%TYPE,
   po_cursor   OUT SYS_REFCURSOR
)
```

### Indentation

-  Use space formatted text not TAB formatted text. Spaces are consistently understood by different text editors whereas TABs are not
-  Use three spaces to indent each level
-  If you change a block of code make sure that the whole block is still correctly indented when you have finished 
-  Make sure indentation is consistent. For example it can be very confusing to see a misaligned multi IF-ELSEIF-ENDIF statement


```
BEGIN
   OPEN po_cursor FOR
        SELECT et.example_date,
               tt.offender_id
          FROM example_table et 
               INNER JOIN test tt ON et.example_id = tt.example_id
         WHERE et.example_from_date = pi_from_date
           AND tt.test_date <> l_sysdate
      ORDER BY et.example_date DESC;
END ro_example;
```

### Case Formatting

-  Uppercase
      -  Keywords
      -  Oracle built ins e.g. DECODE
      -  Oracle built in packages e.g. DBMS_OUTPUT
-  Lowercase
      -  Identifiers

  
```
SELECT et.example_date,
         tt.offender_id
    FROM example_table et 
         INNER JOIN test tt ON et.example_id = tt.example_id
   WHERE et.example_from_date = pi_from_date
     AND tt.test_date <> l_sysdate
ORDER BY et.example_date DESC;
```

### Operators and Punctuations

-  Have a space before parameter or column lists
-  Stack operators AND, OR 
    -  The operators are to the left 
    -  The conditions are aligned when stacked
    -  The operators are aligned with the DML keyword
-  Wrap plus(+), minus(-), multiply (*) and divide (/) operators 
-  String concatenation is stacked with the operators (||) to the left


```
SELECT ename
       || ', '
       || empno
          AS employee,
       sal / 2 * 10 * 12 + 5 - 42 AS weekly_income,
       hiredate,
       deptno
  FROM scott.emp
 WHERE emp.hiredate > TO_DATE ('1982', 'YYYY')
   AND emp.deptno = 20
```

###  List Arrangements

-  Commas are trailing
-  For parameters when there is more than two values in the lists, each value is listed on a new line in alignment with the value above
-  For partition lists the values are wrapped
-  For all other lists when there is more than one value in the list, each value is listed on a new line in alignment with the value above


```
SELECT first_name,
       last_name,
       date_of_birth,
       reference_number
  FROM offender_detail
 WHERE offender_name = 'JOHN';
```

## Visual Studio ‘TFS’ Source Control

When checking in code (either adding new or altering existing) into the source control system always add a relevant, concise comment describing the new code or the changes to the existing code. Include, where possible, reference to the service request, bug or issue e.g. SR1234 or Defect 789.

No object should ever be added to source control if it does not compile.

###  Property Map

All scripts require a Property Map to ensure that the script is picked up by the build and release process. The Property Map must be at the top of each script below the object name and parameters. The values for the Property Map are:

-  Scriptid – the file name for the script
-  Type – contains one of the following values:
    -   Procedure
    -   Function
    -   Package body
    -   Package
-  Name – name of script
-  Order – sets the order the script is to run in, in relation to other scripts in the release
-  Release – set to ‘yes’ if the script is to be included in the build


```
CREATE OR REPLACE PACKAGE BODY <pkg_package_name> IS
  --begin_property_map
  --scriptid=pkg_ package _name.sql
  --type=package
  --name=pkg_ package _name
  --order=1
  --release=yes
  --end_property_map
```

## Syntax

Where Oracle supports the ANSI SQL syntax this must be used over synonymous Oracle specific syntax e.g. use SELECT DISTINCT instead of SELECT UNIQUE, and LEFT OUTER JOIN instead of (+).

##Data Types and Lengths

Be aware of the data type that is being used and referenced in the code e.g. do not declare a local variable as a type of VARCHAR2 (32760) and then assign a one number value of ‘9’ to it. In this case it should be a NUMBER (1).

When creating a date always specify the format mask e.g. ‘DD/MM/YYYY’. Do not rely on the DBMS default date format.

##  Naming Standards

The names for objects must be clear and define what the object represents. Be as explicit as possible when naming objects especially variables, as this will go some way to reduce incorrect assumptions being made about their purpose. Object names must be singular e.g. ro_offender_detail not ro_offender_details, with the exception of collections (e.g. Nested Tables).

All object names must have a prefix which identifies the type of object, e.g. ‘pkg’ for packages, ‘fn’ for functions, ‘l’ for local variables, as per the table below.

![image.png](.attachments/image-6afacee9-982c-408e-a994-e91f8bb82b57.png)

##  Parameters, Variables and Constants

Always use the %TYPE attribute to declare variables, constants and parameters which are PL/SQL representations of database values. This ensures that your variables stay synchronised with the database structure. Always reference the correct table and column.  Only specify the datatype if the variable is not related to a table and column.


```
pi_task_id IN person_task.task_id%TYPE
l_first_name person.first_name%TYPE;
lc_person_status CONSTANT person.status%TYPE := 'ACTIVE';
l_count NUMBER;
```
### Parameters

Place the parameters immediately after the procedure/function create statement, and then include the property map and comments. Parameters for procedures should always be explicitly declared with IN, OUT, or IN OUT. Functions must not have OUT or IN OUT parameters.


```
CREATE OR REPLACE PROCEDURE ro_example
(
   pi_from_date IN DATE,
   pi_to_date IN table_name.column_name%TYPE,
   po_cursor   OUT SYS_REFCURSOR
)
  --begin_property_map
  --scriptid=ro_example.sql
  --type=procedure
  --name=ro_example
  --order=1
  --release=yes
  --runonce=no
  --end_property_map
/***************************************************************
    Name:         ro_example
    Project:      Example Project
    Design Ref:   Example – Design Doc V1.0 (TRIM Ref D99-9) 
    Purpose:      ro_example was created to...
    Comments:     Overarching business rules used...
    Screen(s):    Example Menu>Example Screen             
    Created By:   Joe Bloggs
    Created Date: 06/05/2014
    ------------------------------------------------------------
    Modification History
    ------------------------------------------------------------
    When        Who        Specification Change/TFS Bug   
                            No/Details of Change
    ----------  ---------  -------------------------------------       
****************************************************************/
```
###  Variables

Each variable should have one purpose and one purpose only. The name for that variable should describe, as clearly as possible, that single purpose. If you no longer use a variable, it should be removed.

###  Constants

When declaring variables, determine if the value is going to change or not, if not then declare it as a constant. This aids maintenance as you do not need to work through the script to determine the value at a given point.

##	Aliases
###  Table Aliases

If a SQL statement references more than table, each table must be given a table alias as this makes the code easier to read and avoids errors when specifying common column names. Use meaningful names such as acronyms or abbreviations e.g. for eh_aggregate_sentence use agg, or eh_board_hearing_order use bho. Don’t use xyz, 123, a, b, 1 etc.

###  Column Aliases

Aliases are used to make the column headings in your result set easier to read. When a column is the output of a function or operation you must use a column alias. For example, when concatenating fields together, you should alias the result.


```
SELECT UPPER (ofdr.last_name)
       || ', '
       || ofdr.first_name
          AS offender_name
  FROM offender ofdr;
```
Always use AS before the column alias as it makes it much easier to read.

##  Database Transactions

A transaction is defined as a single logical operation e.g. a transfer of funds from one bank account to another, even involving multiple changes such as debiting one account and crediting another, as a single transaction. 

All database transactions must be atomic, i.e. each transaction must be “all or nothing”. If one part of the transaction fails, the whole transaction fails and no changes are made to the database.

## Oracle SQL Functions

SQL functions are built into Oracle and provide a useful and efficient method for transforming data. There are too many SQL functions to mention here however a good reference point is Oracles own books online library: [http://docs.oracle.com/cd/B19306_01/server.102/b14200/functions001.htm]()

## The Dangers of Oracle SQL Functions

Using any function on an indexed column of a table within the search argument (WHERE) clause will prevent the Oracle query optimizer from using that index. This severely affects the query performance. The only exception to this is that when a function based index exists and the function is the same as that used within the SQL statement.

It may be necessary at times to perform an RTRIM() on a fixed length (CHAR) column in the search argument (WHERE) clause.

If an idempotent function is to be called multiple times with the same parameter(s) in PL/SQL consider executing this once and storing the value in a local variable for comparison. If you need to match an input parameter date of type varchar2 to a column containing an Oracle date, convert and store the input parameter as an Oracle date then compare using this variable.

Avoid unnecessary use of functions where they are not needed.

**Examples of unnecessary function use:**

![image.png](.attachments/image-fcc1adb0-9b34-4254-abe2-3d858e688702.png)

##  The use of ‘UNION’ & ‘UNION ALL’

UNION returns all distinct rows selected by either query and UNION ALL returns all rows selected by either query, including all duplicates.

Always use UNION ALL where distinct rows are not required as it has better performance.

##  VARCHAR – Never Use

Never use the Oracle datatype VARCHAR. Always use VARCHAR2. 

## Cursors

There are two types of cursors available, implicit cursors and explicit cursors.

### Implicit Cursors

Implicit cursors are automatically created by Oracle whenever an SQL statement is executed, they:
-  Are faster than explicit cursors
-  Automatically include a TOO_MANY_ROWS and NO_DATA_FOUND exception check
-  Automatically close when all data has been read or an exception has been raised

**Implicit cursor for loop example:** 
```
BEGIN
   FOR rec IN (SELECT ep.empno
                 FROM scott.emp ep
                WHERE sal > 1000)
   LOOP
      DBMS_OUTPUT.put_line (rec.empno);
   END LOOP;
END;
```
###  Implicit Cursors – SELECT INTO

SELECT INTO should be used when one row is expected. If the query does not return a single row an exception will be raised for TOO_MANY_ROWS or NO_DATA_FOUND.

**For Example:** 
```
SELECT 'Y'
  INTO l_hrx_flag
  FROM high_risk_offender
 WHERE offender_id = pi_offenderid
   AND end_date IS NULL;
```
In the code it is incorrectly assuming there is only one occurrence of an offender record in the table where the end_date isn’t present.

In the code below the COUNT aggregate function is used, as aggregate functions always return a single row this code will never return NO_DATA_FOUND or TOO_MANY_ROWS.


```
SELECT DECODE (COUNT (1), 0, 'N', 'Y')
  INTO l_hrx
  FROM high_risk_offender
 WHERE offender_id = pi_offenderid
   AND end_date IS NULL;
```

### Explicit Cursors

Explicit cursors are manually created in PL/SQL. They should only ever be used when the query is to be reused.

**Explicit cursor example:**


```
DECLARE
   CURSOR employees_cur
   IS
      SELECT ep.empno
        FROM scott.emp ep
       WHERE sal > 1000;

   l_emp_no scott.emp.empno%TYPE;
BEGIN
   OPEN employees_cur;

   LOOP
      FETCH employees_cur INTO l_emp_no;

      EXIT WHEN employees_cur%NOTFOUND;

      DBMS_OUTPUT.put_line (l_emp_no);
   END LOOP;

   CLOSE employees_cur;
END;
```
##  GOTO Statements

Never use GOTO statements.

##  Globals

Do not use Oracle globals as they obscure the flow of data through the code and can retain values from one execution of the code to the next.
Data must be passed inside and between packages as explicit parameters rather than using globals as implicit parameters.

##  Dynamic SQL

Dynamic SQL must never be used as it has the following limitations:
-  It must always be parsed 
-  It cannot be fully traced and profiled 
-  It cannot be pinned
-  It cannot be precompiled
-  It suffers from network overhead
-  It is open to SQL Injection exploitation

**Example of dynamic SQL:**

```
CREATE OR REPLACE PROCEDURE ro_search_employees
(
   pi_employee_name IN scott.emp.ename%TYPE,
   pi_employee_number inscott.emp.empno%TYPE,
   pi_departno inscott.emp.deptno%TYPE,
   po_cursor   OUT SYS_REFCURSOR
)
AS
   lc_search_prefix CONSTANT VARCHAR2 (78) DEFAULT 'SELECT se.empno, se.ename, se.job, se.sal, se.deptno FROM scott.emp se WHERE ';
   l_search_filters VARCHAR2 (32767);
BEGIN
   --Add filter for eployee name if it is avalible
   IF pi_employee_name IS NOT NULL
   THEN
      l_search_filters :=
         l_search_filters
         || 'AND SE.ENAME = '
         || pi_employee_name;
   END IF;

   --Add fitler for employee number if it is avalible
   IF pi_employee_number IS NOT NULL
   THEN
      l_search_filters :=
         l_search_filters
         || 'AND SE.EMPNO = '
         || pi_employee_number;
   END IF;

   --Add filter for employee department number if it is avalible
   IF pi_departno IS NOT NULL
   THEN
      l_search_filters :=
         l_search_filters
         || 'AND SE.DEPTNO = '
         || pi_departno;
   END IF;

   --remove leading AND from concatinated search filters
   l_search_filters :=
      SUBSTR (l_search_filters,
              5
             );

   --Dynamic SQL example 1: dynamic cursors.
   OPEN po_cursor FOR
      lc_search_prefix
      || l_search_filters;
END;
```

**Example of Static Equivalent**

```
CREATE OR REPLACE PROCEDURE ro_search_employees
(
   pi_employee_name IN scott.emp.ename%TYPE,
   pi_employee_number inscott.emp.empno%TYPE,
   pi_departno inscott.emp.deptno%TYPE,
   po_cursor   OUT SYS_REFCURSOR
)
AS
BEGIN
   --One option for dy
   OPEN po_cursor FOR
      SELECT se.empno,
             se.ename,
             se.job,
             se.sal,
             se.deptno
        FROM scott.emp se
       --if provided, ensure employee name matches
       WHERE (pi_employee_name IS NULL
          OR  se.ename = pi_employee_name)
         --if provided, ensure employee number matches
         AND (pi_employee_number IS NULL
          OR  se.empno = pi_employee_number)
         --if provided, ensure department number matches
         AND (pi_departno IS NULL
          OR  se.deptno = pi_departno);
END;
```
##  Overloading

Overloading is used to enable the same function/procedure name for different data types. Never over load, and instead use separate explicitly named functions/procedures.

##  Select *

Never use SELECT * in retrieval procedures. Specify the columns you want to return as:

-  The code is easier to understand, which means you need fewer comments and maintenance will be faster and therefore cheaper
-  Result sets may change over time as the tables are altered
-  Using SELECT * in a sub query removes the option of index cover on a query
-  It could save the need for changes in the future. Columns may be added to or removed from the table, but the order of the columns could well change (unlikely in a table but not impossible).
-  Network traffic is reduced which improves performance if the table has a large number of columns, or has a BLOB or CLOB column

##  Exception Handling

Exception handling should be handled in the application layer. The only time an exception should be caught and handled in PL/SQL is if:

-  The exception is a foreseeable expected behaviour of the functionality
-  The script is to be run as a scheduled job directly within the database, in which case the exception needs to be logged

When handling +expected exceptions ensure only the expected exception is caught, e.g. NO_DATA_FOUND not OTHERS

The below code is a bad example of exception handling which will never pass a peer review.

```
EXCEPTION
   WHEN OTHERS
   THEN
      NULL;
END;
```

The below code is an example of acceptable exception handling.
```
FUNCTION emp_salary (pi_emp_id scott.emp.empno%TYPE)
   RETURN scott.emp.sal%TYPE
IS
   l_salary scott.emp.sal%TYPE;
BEGIN
   SELECT emp.sal
     INTO l_salary
     FROM scott.emp emp
    WHERE emp.empno = pi_emp_id;

   RETURN l_salary;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      RETURN 0;
END;
```

## Records

Records must only contain columns that are being used by the code. Avoid using syntax such as %ROWTYPE when some of the columns are not used. All data types within the record should use the %TYPE syntax where possible.

##  Collections

Nested tables and FORALL loops must be used when performing a bulk insert/update/delete from a collection. FORALL loops have a single context switch from PLSQL to SQL which make them more efficient than regular loops.

```
--Package Header

CREATE OR REPLACE PACKAGE plsql_collection_example
AS
   TYPE t_department_row IS RECORD
   (
      deptno scott.dept.deptno%TYPE,
      dname scott.dept.dname%TYPE,
      loc scott.dept.loc%TYPE
   );

   --Create nested table
   TYPE t_department IS TABLE OF t_department_row;

   PROCEDURE update_example (pi_departments IN t_department);
   
END plsql_collection_example;
/

--Package Body

CREATE OR REPLACE PACKAGE BODY plsql_collection_example
AS
   PROCEDURE update_example (pi_departments IN t_department)
   IS
   BEGIN
      --Update all records
      FORALL idx IN pi_departments.FIRST .. pi_departments.LAST
         UPDATE scott.dept dp
            SET dp.dname = pi_departments (idx).dname,
                dp.loc = pi_departments (idx).loc
          WHERE dp.deptno = pi_departments (idx).deptno;
   END update_example;
   
END plsql_collection_example;
/
```
## 1.3 General Database Developmement
## Defaults

There must be no default values used in NULLABLE columns. 

If a column is currently NOT NULL and it should allow null values then contact the Corrections Data Architect to who will arrange for it to be changed.

There are existing cases of this throughout the EPIC schema with ‘0’ on numeric columns, ‘_NOSP’ on character columns and ‘00/00/0000’ or ‘99/99/9999’ on date columns. This behaviour must not be repeated on anything new and where possible stopped if existing code is updated.

## Multiple Updates

Within a logical record set only update the record or records that have changed. For example if a grid of eight records is displayed but only two records of the grid change then only update those two records. Avoid the situation where the whole logical record set is updated. 

Another example is parent-child relationships; if only the child record is updated you do not need to update the parent record e.g. the REQUEST table has a child REQUEST_STATUS table. If the request status changes only update the child table REQUEST_STATUS. REQUEST does not need to be updated. 

##  Dates

###  EPIC Dates vs Oracle Dates

The EPIC schema stores dates as twenty character string in the data type CHAR. The date is stored in the following format:

-  YYYYMMDD HH24:MI:SS+999
    -  YYYY denotes the year – 2009
    -  MM denotes the month  – 07
    -  DD denotes the day – 01
    -  HH24 denotes the hour in 24-hour format
    -  MI denotes the minutes
    -  SS denotes the seconds
    -  +999 denotes the daylight savings offset in number of minutes passed GMT. They are usually held as either ‘+720’ minutes (12hrs past GMT) or ‘+780’ minutes (13hrs past GMT).

The IOMS, CORR and ALTS schemas store dates as a data type of DATE which has a default format defined at database level that can be formatted into pre-defined string formats e.g. ‘DD/MM/YYYY,  DD Month YY HH:MI am/pm, DD-MON-YY etc. This DATE data type should be utilised for storing, calculating and comparing dates.

### Converting EPIC Dates

To compare EPIC dates with Oracle dates always convert the EPIC dates to Oracle dates first, do not convert the Oracle dates to strings and compare strings.
Use the custom functions for converting EPIC dates to and from Oracle dates:

![image.png](.attachments/image-f787a2d3-7203-459a-9dbb-d12120263e36.png)

###  Date vs Date Time

The data model in IOMS and EPIC specifies via the column description whether or not the time component of a date should be captured. In CORR and ALTS this is indicated by the column name e.g. START_DATE vs START_DATETIME.

##  Identity Identifiers

Do not display surrogate keys. Firstly this information has no meaning to users and secondly it provides a security risk to publish identifiers which may allow access to data.

##  Flags

In EPIC and IOMS the agreed standard for variables that being used as Boolean flags is to declare a CHAR (1) variable. In CORR and ALTS it is VARCHAR2 (1).
In IOMS, flags are stored as NOT NULL with only two values:
  -  ‘0’ = no 
  -  ‘1’ = yes
In EPIC, flag columns can either be NOT NULL or NULL. Typically the values ‘N’ and ‘Y’ are stored though in some cases other values may be held.
  -  ‘N’ = no 
  -  ‘Y’ = yes
  -  Other values may exist e.g. ‘1’, ‘X’, ‘P’

In CORR and ALTS, flags are stored as NOT NULL with only two values:
  -  ‘N’ = no 
  -  ‘Y’ = yes

##  Lookups

Most lookup values are stored in a central table in each schema as follows:
  -  CORR.CR_REF_LOOKUP
  -  IOMS.OM_REF_LOOKUP
  -  EPIC.EPIC_LOOKUPS

However, there are cases where the lookup values have their own tables as they do not fit into the predefined structure. For example:
  -  CORR.CR_LOCATION
  -  IOMS.OM_REF_CIE_COST_CENTRE
  -  EPIC.EPIC_LOCATIONS

In EPIC and IOMS to retrieve the description for a lookup code you also need to know the look class, as the code is only unique within a class. There are existing functions in IOMS and EPIC that can be used to return the lookup description.  These functions are ioms.fn_lookup_text and epic.fn_lookup_text. For example:

```
IOMS.fn_lookup_text('CLASS NAME', 'CODE NAME', 'IC');
EPIC.ef_lookup_text('CLASS NAME', 'CODE NAME', 'UC');
```

In CORR each lookup value has a unique identifier regardless of its class or code. This is the value that should be used to find a lookups description. The function corr.fn_lookup_desc can be used for this purpose.  For example:

 
`CORR.fn_lookup_desc('LOOKUP ID', 'UC');`
 

Code and class values must not be visible to the user. Other rules for lookups displayed in the application are:

-  On new
    -  End dated values do not appear in the drop down lists. The only exception to this is offence codes as it is valid to add historical values
    -  Future dated values do not appear in the drop down lists
-  On edit
    -  End dated values do not appear in the drop down lists. The exceptions are:
        -  Offence codes 
        -  Any value the record was saved with even if it has subsequently being end dated this end dated value  must also be available for selection
    -  Future dates values do not appear in the drop down lists

##  Common Table Columns

There are two common groups of table columns used throughout the database, the action columns which are on every table and the created/updated columns which are only on the tables which require this information to be captured.

###  Action Columns

The ACTION columns must never be used by the application as these columns are also updated by non-business processes which have no meaning to the business user, e.g. offender merge, data fixes, data migration.

![image.png](.attachments/image-de04dc4c-1918-468f-b622-34b48b825681.png)

###  Created/Updated Columns

The created/updated columns capture the user who created or last updated the record and the date and time this was action was taken. Unlike the action columns these only capture changes to the record by users through the application.

![image.png](.attachments/image-41b0bca7-2fdb-4897-91fc-7da36b8db250.png)

In EPIC and IOMS use the function epic.ef_get_username to get the name of the user who created or updated the record.

`epic.ef_get_username (updated_by)`
 
In CORR and ALTS use the function corr.fn_get_users_name.
 
`corr.fn_get_users_name (created_by_id, 'LAST, First')`
## 1.4 Performance Tuning 
# SQL and PL/SQL Performance Tuning

Efficient SQL and PL/SQL is important for all data development scripts. 

Inefficient PL/SQL used by the application will result in slow response times giving the end-user an impression of a poorly designed system. Inefficient scripts, such as data fix’s and batch process, will hold valuable CPU and memory resources on the server which can potentially impact other sessions.

Performance tuning isn’t an optional extra that a select group of developers carry out, it should always be carried out by ALL developers as part of our normal development process, and will be included as part of the code peer review process.

In most cases the standards in this document can be met whilst providing efficient code. If this is not the case then maintainability takes precedence over efficiency, it is easier to update maintainable code for efficiency than it is to update the functionality of efficient poorly designed code.

##  SQL Performance Tuning Tips

-  Keep the number of tables joined to the minimum necessary
-  Keep the amount of data retrieved to the minimum necessary
-  As a first priority always join tables by the primary key/foreign key relationship
-  If passing search parameters from the application, include a primary key identity if available rather than searching by less efficient criteria. This may mean reading the PK identity in the first place, and passing it as a parameter in the search criteria
-  Make sure you utilise index coverage on your search criteria
-  Identify missing indexes and request creation by contacting the Corrections Data Architecture team  
-  Always generate and review the ‘Explain Plan’ for every block of SQL code

##  PL/SQL Performance Tuning Tips

-  Use implicit cursors over explicit cursors
-  Take advantage of short-circuit evaluation. In a Boolean expression, place the least expensive arguments to the left of the expression. Expressions are resolved from left to right and if the result can be determined before the expression is complete then the remainder of the expression will not be computed

## 1.5 










## 2. Microsoft .NET Coding Standards
## 2.1 Naming conventions 
# General Guidelines

- Always use Camel Case or Pascal Case names.  Refer page [Naming Convention](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Avoid ALL CAPS and all lowercase names. Single lowercase words or letters are acceptable.
- Do not create declarations of the same type (namespace, class, method, property, field, or parameter) and access modifier (<font color="blue">protected, public, private, internal </font>) that vary only by capitalization.
- Do not use names that begin with a numeric character.
- Do add numeric suffixes to identifier names.
- Always choose meaningful and specific names.
- Variables and Properties should describe an entity not the type.
- Avoid using abbreviations unless the full name is excessive.
- Avoid abbreviations longer than 5 characters.
- Any Abbreviations must be widely known and accepted.
- Use uppercase for two-letter abbreviations, and Pascal Case for longer abbreviations.
- Do not use C# reserved words as identifiers.
- Avoid naming conflicts with existing .NET Framework namespaces, or types.
- Avoid adding redundant or meaningless prefixes and suffixes to identifiers

Good Example:

        public enum ColorsEnum {…}
        public class ColouredVehicle {…}
        public struct RectangleStruct {…}
- Do not include the parent class name within a property name.
Example: <font color="blue">Offender.Name</font> NOT <font color="blue">Offender.OffenderName</font>
- Try to prefix Boolean variables and properties with “Can”, “Is” or “Has”.
- Append computational qualifiers to variable names like <font color="blue">Average, Count, Sum, Min</font>, and <font color="blue">Max</font> where appropriate.
- When defining a root namespace, use a Product, Company, or Developer Name as the root. Example: <font color="blue">Corrections.IOMS.DocumentTemplate.WebUI</font>

# Name Usage and Syntax

This section provides name and syntax used across all .Net types, Projects, Source files and namespaces.

## Poject File

- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)  
- Always match Assembly Name & Root Namespace.
```
Example:
LanceHunt.Web.csproj -> LanceHunt.Web.dll -> namespace LanceHunt.Web
```

## Source File

- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Always match Class name and file name. 
- Avoid including more than one Class, Enum (global), or Delegate (global) per file. 
- Use a descriptive file name when containing multiple Class, Enum, or Delegates.
```
Example:
MyClass.cs
=>
public class MyClass
{…}
```
##Resource or Embedded File
- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Use a name describing the file contents.

##Namespace
- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Try to partially match Project/Assembly Name.

```
Example:
namespace LanceHunt.Web
{…}
```
## Class or Struct
- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Use a noun or noun phrase for class name.
- Add an appropriate class-suffix when inheriting another type when possible.


```
Examples:
private class MyClass
{…}
internal class SpecializedAttribute : Attribute
{…}
public class CustomerCollection : CollectionBase
{…}
public class CustomEventArgs : EventArgs
{…}
private struct ApplicationSettings
{…}
```
##Interface	

- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Always prefix interface name with capital “I”.


```
Example:
interface ICustomer
{…}
```


##Generic Parameter Type 

Always use a single capital letter, such as T or K.


```
Example:
public class FifoStack<T>
{
   public void Push(<T> obj)
   {…}
   
   public <T> Pop()
   {…}
}
```


##Method	
- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Try to use a Verb or Verb-Object pair.


```
Example:
public void Execute() {…}
private string GetAssemblyVersion(Assembly target) {…}
```


## Property	 
- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Property name should represent the entity it returns. Never prefix property names with “Get” or “Set”.


```
Example:
public string Name
{
  get{…}
  set{…}
}
```

## Field (Public, Protected or Internal)

- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Avoid using non-private Fields! Use Properties instead.


```
Example:
public string Name;
protected IList InnerList;
```

##Field(Private)	

- Use [camel Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention) and prefix with a single underscore (_) character.


```
Example:
private string _name;
```


##Constant or Static Field

- Treat like a Field.
- Choose appropriate Field access-modifier above.

## Enum	

- Use [Pascal Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention) (both the Type and the Options).


```
Example:
public enum CustomerTypes
{
  Consumer,
  Commercial
}
```


## Delegate or Event

- Treat as a Field.
- Choose appropriate Field access-modifier above.


```
Example:
public event EventHandler LoadPlugin;
```


## Variable (inline)
- Use [camel Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Avoid using single characters like “x” or “y” except in FOR loops.
- Avoid enumerating variable names like text1, text2, text3 etc.

## Parameter

- Use [camel Casing](/Development-Standards/Microsoft.NET-Coding-Standards/Naming-Convention)
- Do not prefix parameter names with Hungarian type notation


```
Example:
public void Execute(string commandText, int iterations)
{…}
```
## 2.3 Coding Standards
Coding style causes the most inconsistency and controversy between developers. Each developer has a preference, and rarely are two the same. However, consistent layout, format, and organization are key to creating maintainable code. The following sections describe the preferred way to implement C# source code in order to create readable, clear, and consistent code that is easy to understand and maintain.

#Formatting

1. Never declare more than 1 namespace per file.
1. Avoid putting multiple classes in a single file.
1. Always place curly braces ({ and }) on a new line.
1. Always use curly braces ({ and }) in conditional statements.
1. Always use a Tab & Indention size of 4.
1. Place namespace “using” statements together at the top of file. Use the remove and sort option available with VS.NET on the namespaces in the code file
1. Group internal class implementation by type in the following order
   - Member variables.
   - Constructors & Finalizers.
   - Nested Enums, Structs, and Classes.
   - Properties
   - Methods   
1. Sequence declarations within type groups based upon access modifier and visibility:
   - Public
   - Protected
   - Internal
   - Private
1.	Segregate interface Implementation by using #region statements.
1.	Append folder-name to namespace for source files within sub-folders.
1.	Recursively indent all code blocks contained within braces.
1.	Use white space (CR/LF, Tabs, etc) liberally to separate and organize code.
1.	Add a whitespace around operators, like +, -, ==, etc.
1.	Only declare related attribute declarations on a single line, otherwise stack each attribute as a separate declaration.

```
Example:
// Bad!
[Attrbute1, Attrbute2, Attrbute3]
public class MyClass
{…}
// Good!
[Attrbute1, RelatedAttribute2]
[Attrbute3]
[Attrbute4]
public class MyClass
{…}
```
15.	Place Assembly scope attribute declarations on a separate line.
16.	Place Type scope attribute declarations on a separate line.
17.	Place Method scope attribute declarations on a separate line.
18.	Place Member scope attribute declarations on a separate line.
19.	Place Parameter attribute declarations inline with the parameter.
20.	Don’t indent lambda statements and use a format like this:

```
methodThatTakesAnAction.Do(x => 
{ 
	// do something like this 
}
```
21.	Put the entire LINQ statement on one line, or start each keyword at the same indentation, like this:

```
var query = from product in products where product.Price > 10 select product; 

// or
 
var query = 
	from product in products 
	where product.Price > 10 
	select product;
```
# Code Commenting

The below points covers the code commenting for better understanding
1.	 All comments should be written in the same language, be grammatically correct, and contain appropriate punctuation
2.	Only use C# comment-blocks for documenting the API.
3.	Use // or /// but never /* … */
4.	Do not “flowerbox” comment blocks.

```
Example:
// ***************************************
// Comment block
// ***************************************
```

5.	Comments should add to the clarity of your code. Keep comments simple. Write comments before you write code.
6.	Document why something is being done, not just what. Write comments wherever required. But good readable code will require very less comments. If all variables and method names are meaningful, that would make the code very readable and will not need many comments.
7.	If you have to use some complex or unusual logic for any reason, document it very well with sufficient comments. Use inline-comments to explain assumptions, known issues, and algorithm insights
8.	Include comments using Task-List keyword flags to allow comment-filtering.

```
Example:
// TODO: Place Database Code Here
// UNDONE: Removed P\Invoke Call due to errors
// HACK: Temporary fix until able to refactor
```

9.	Include <summary>, <param>, <return>, <remarks>, <exception>, <see cref=""/> and <see cref=””/>  and <seeAlso cref=""/> comment tags wherever applicable.
10.	Always add CDATA tags to comments containing code and other embedded markup in order to avoid encoding issues.

```
Example :
/// <example>
/// Add the following key to the “appSettings” section of your config:
/// <code><![CDATA[
/// <configuration>
/// <appSettings>
/// <add key=”mySetting” value=”myValue”/>
/// </appSettings>
/// </configuration>
/// ]]></code>
/// </example>
```

#File Header

All files should start with the given standard header. This is meant to give an idea about the contents of the file and what sort of functionality, it supports.


```
// ==============================================================
// User Interface Application Block for IOMS
//
// <Class file name>.cs
//
// comment the implementation detail about this file 
// 
//==============================================================
// Copyright (C)
// All rights reserved.
//==============================================================
// Change History
// -------------------------------------------------------------
// Date		Edit	Author	Comment	Change	Function
//						
// -------------------------------------------------------------
// 
// -------------------------------------------------------------
```
#Class Header
Every class should start with the following standard header to give a brief idea about the purpose of the class.
```
/// <summary>
/// Describe the purpose of the class
///<summary>
```
#Function Header
The function headers will be used to automatically generate content for the API Reference Manual.  The function headers should be in the following template

```
/// <summary>
/// Describe the purpose of the method.  
/// For example, This Method is used for Adding Two Integer 
/// Variables.
/// </summary>	
/// <Describe the purpose of the parameters	>
/// <param name="a">Used to get the First Input Value</param>
/// <param name="b">Used to get the Second Input Value</param>
/// Describe the purpose of the return value(s)
/// <returns>Returns the Sum of the Input Values</returns>	
/// Provide any additional information in the remarks section
/// <remarks>
/// <example>
/// <code>
/// This sample shows how to call the Hello method.
/// <pre>
/// class API_Ref
/// {
///  	public void MethodName()
///     	{
///    		return Hello(2, 3);
///     	}
/// }
/// </pre>
/// </example>
/// </remarks>
```
```
private void FillRequestInformation()
{
//---------------------------------------------------------------
// Change History
//---------------------------------------------------------------
// Date           Edit   Author    Comment		
//---------------+-----+---------+-------------------------------
// DD-MMM-YY              X     Created
//---------------+-----+---------+-------------------------------
}
```
#Comments for Variable
The variable declaration should hold a brief description about the purpose of the variable.


```
Example:
string sUserName; 		///holds name of the user
```


Comments should be properly aligned along a single vertical line.

## 3. Guidelines and Best Practices
## 3.1 C Sharp Guidelines 
# General 

1.	Do not omit access modifiers. Explicitly declare all identifiers with the appropriate access modifier instead of allowing the default.

```
Example:
// Bad!
Void WriteEvent(string message)
{…}
// Good!
private Void WriteEvent(string message)
{…}
```


2.	Do not use the default (“1.0.*”) versioning scheme. Increment the AssemblyVersionAttribute value manually.
3.	Set the ComVisibleAttribute to false for all assemblies
4.	Only selectively enable the ComVisibleAttribute for individual classes when needed.

```
Example:
[assembly: ComVisible(false)]
[ComVisible(true)]
public MyClass
{…}
```
5.	Consider factoring classes containing unsafe code blocks into a separate assembly.
6.	Avoid mutual references between assemblies.
7.	Always check a delegate for null before invoking
8.	Avoid events as interface members
9.	Prefer using explicit interface implementation
10.	Never hardcode strings that will be presented to end users. Use resource files instead.
11.	Never hardcode a path or drive name in code. Get the application path programmatically and use relative path. 
12.	Have your own templates for each of the file types in Visual Studio. You can include your company name, copy right information etc in the template. You can view or edit the Visual Studio file templates in the folder C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\ItemTemplatesCache\CSharp\1033. (This folder has the templates for C#, but you can easily find the corresponding folders or any other language)
13.	Build with the highest warning level.  Configure the development environment to use Warning Level 4 for the C# compiler, and enable the option Treat warnings as errors. This allows the compiler to enforce the highest possible code quality with 0 errors and 0 warnings
14.	Avoid suppressing specific compiler warnings
15.	Automate the design reviews with FxCop and Visual Studio Analysis. All  code analysis rules to be followed in the code analysis except for the below rules.  Some of the rules may not be turned on for some projects.  Any deviations will be addressed as aprt of that project as a deviation from this standard and approved by the Department.
    - **CA1002**	Do not expose generic list
    - **CA1020**	Avoid namespaces with few types
    - **CA1021**	Avoid out parameters
    - **CA1024**	Use properties where appropriate
    - **CA1304**	Specify CultureInfo
    - **CA1305**	Specify IFormatProvider
    - **CA1307**	Specify StringComparison
    - **CA1707**	Identifiers should not contain underscores
    - **CA1822**	Mark members as static
    - **CA2227**	Collection properties should be read only
16.	Eliminating code duplication through refactoring to design patterns to enhance the application code
17.	Integrate testing and coding to organizing, coordinating and running test cases with Visual Studio
18.	Automate Unit test to improve quality with consistent test coverage using Moq framework only when there are interfaces involved or business logic or rules are componentized. At the minimum, DAL components should hit the database when using ADO.NET stored procedures as business logic is written in stored procedures and these should be unit tested too
19.	All the methods should have proper Xml comments
20.	Remove unused “Using” statements (use Remove and Sort feature of VS.NET)
21.	Ensure Unit test and test coverage  a minimum of 85%
22.	Unit tests should cover all layers – UI, Services and Components
23.	Use anonymous types where applicable
24.	Ensure meaningful log statements with unique error numbers where applicable and different levels of logging.  Use Info statements for main events. Do not repeat statements for Debug and Info logs.  For debug statements alone, parameters can be passed for better diagnosis.
25.	No dead configuration entries. Names should be meaningful and not copy pasted from other projects
26.	No duplicated variables or code anywhere
27.	Use XSD.exe to generate strongly typed classes for Xml manipulation where applicable
28.	Use a single class or a component for configuration settings

#Variable and Types
The below provide declaration and usage of variables and types and do’s and don’ts

1.	Try to initialize variables where you declare them.
2.	Declare variables just before usage
3.	Declare variables once for stateful classes and reuse rather than creation across multiple methods repeatedly
4.	Always choose the simplest data type, list, or object required.
5.	Always use the built-in C# data type aliases, not the .NET common type system (CTS).

```
Example:
short NOT System.Int16
int NOT System.Int32
long NOT System.Int64
string NOT System.String
```
6.	Only declare member variables as private. Use properties to provide access to them with public, protected, or internal access modifiers.
7.	Try to use int for any non-fractional numeric values that will fit the int datatype - even variables for non-negative numbers.
8.	Only use long for variables potentially containing values too large for an int.
9.	Try to use double for fractional numbers to ensure decimal precision in calculations.
10.	Only use float for fractional numbers that will not fit double or decimal.
11.	Avoid using float unless you fully understand the implications upon any calculations.
12.	Try to use decimal when fractional numbers must be rounded to a fixed precision for calculations. Typically this will involve money.
13.	Avoid using sbyte, short, uint, and ulong unless it is for interop (P/Invoke) with native libraries.
14.	Avoid specifying the type for an enum - use the default of int unless you have an explicit need for long (very uncommon).
15.	Avoid using inline numeric literals (magic numbers). Instead, use a Constant or Enum
16.	Avoid declaring string literals inline. Instead use Resources, Constants, and Configuration Files, Registry or other data sources.
17.	Declare readonly or static readonly variables instead of constants for complex types.
18.	Only declare constants for simple types.
19.	Avoid direct casts. Instead, use the “as” operator and check for null.

```
Example:
object dataObject = LoadData();
DataSet ds = dataObject as DataSet;
if(ds != null)
{…}
```
20.	Always prefer C# Generic collection types over standard or strong-typed collections. [C#v2+]
21.	Always explicitly initialize arrays of reference types using a for loop.
22.	Avoid boxing and unboxing value types.

```
Example:
int count = 1;
object refCount = count; // Implicitly boxed.
int newCount = (int)refCount; // Explicitly unboxed.
```
23.	Floating point values should include at least one digit before the decimal place and one after.
Example: totalPercent = 0.05;
24.	Try to use the “@” prefix for string literals instead of escaped strings.
25.	Prefer String.Format() or StringBuilder over string concatenation for large string concatenations (>10)
26.	Never concatenate strings inside a loop.
27.	Do not compare strings to String.Empty or “” to check for empty strings. Instead, compare by using String.Length == 0.
28.	Avoid hidden string allocations within a loop. Use String.Compare() for case-sensitive
29.	Convert strings to lowercase or upper case before comparing. This will ensure the string will match even if the string being compared has a different case

```
if (name.ToLower() == “pon”)
{
   //DoSomething
}
```
30.	Avoid using multiple member variables. Declare local variables and pass it to methods instead of sharing a member variable between methods. Sharing a member variable between methods increases the difficulty of tracking the exact method that changed the value
31.	Check for null value of object or variables before using them. This is not required at every place, but wherever the object or variable has the potential to cause a RTE if they are null, but used in say a for loop or assignment statement
```
Example: (ToLower() creates a temp string)
// Bad!
int id = -1;
string name = “lance hunt”;
for(int i=0; i < customerList.Count; i++)
{
   if(customerList[i].Name.ToLower() == name)
      {
   	    id = customerList[i].ID;
      }
}
```

```
// Good!
int id = -1;
string name = “lance hunt”;
for(int i=0; i < customerList.Count; i++)
{
   // The “ignoreCase = true” argument performs a
   // case-insensitive compare without new allocation.
   if(String.Compare(customerList[i].Name, name, true)== 0)
   {
      id = customerList[i].ID;
   }
}
```
32.	Use String.Empty instead of “”;
33.	Use enum wherever required. Do not use numbers or strings to indicate discrete values.

```
// Good!
enum MailType
{
	Html,
	PlainText,
	Attachment
}

void SendMail (string message, MailType mailType)
{
	switch ( mailType )
	{
		case MailType.Html:
			// Do something
			break;
		case MailType.PlainText:
			// Do something
			break;
		case MailType.Attachment:
			// Do something
			break;
		default:
			// Do something
			break;
	}
}
```

```
//Bad 
void SendMail (string message, string mailType)
{
       switch ( mailType )
	{
		case "Html":
	 	       // Do something
			break;
		case "PlainText":
			// Do something
			break;
		case "Attachment":
			// Do something
			break;
		default:
			// Do something
			break;
	}
}
```


34.	Use Lambda expressions instead of delegates.  Lambda expressions provides a much more elegant alternative for anonymous delegate

```
//Delegates
Customer c = Array.Find(customers, delegate(Customer c)
{
   return c.Name == “Tom”;
}

//use an Lambda expression:
Customer c = Array.Find(customers, c => c.Name == “Tom”);

//Or even better
var customer = customers.Where(c => c.Name == “Tom”);
```
35.	The lambda must contain the same number of parameters as the delegate type.
36.	Each input parameter in the lambda must be implicitly convertible to its corresponding delegate parameter.
37.	The return value of the lambda (if any) must be implicitly convertible to the delegate's return type
38.	A variable that is captured will not be garbage-collected until the delegate that references it goes out of scope.
39.	Variables introduced within a lambda expression are not visible in the outer method.
40.	Avoid LINQ for simple expressions
```
//Bad
var query = from item in items where item.Length > 0;

// prefer using the extension methods from the System.Linq namespace, more readable
var query = items.Where(i => i.Length > 0);
```
41.	Only use the dynamic keyword when talking to a dynamic object
Using it introduces a serious performance bottleneck because the compiler has to generate some complex Reflection code. Use it only for calling methods or members of a dynamically created instance (using the Activator) class as an alternative to Type.GetProperty() and Type.GetMethod(), or for working with COM Interop types
42.	Sanitizing user inputs where applicable. Consider using code contracts for Services implementation 
43.	Don’t mix objects when they are nested without null checks and other valid checks in a single statement

#Flow Control
This section covers the flow control using conditions and loops.

1.	Avoid invoking methods within a conditional expression.
2.	Avoid creating recursive methods. Use loops or nested loops instead.
3.	Avoid using foreach to iterate over immutable value-type collections. E.g. String arrays.
4.	Do not modify enumerated items within a foreach statement.
5.	Use the ternary conditional operator only for trivial conditions. Avoid complex or compound ternary operations.
```
Example:
int result = isValid ? 9 : 4;
```
6.	Avoid evaluating Boolean conditions against true or false.
```
Example:
// Bad!
if (isValid == true)
{…}

// Good!
if (isValid)
{…}
```
7.	Avoid assignment within conditional statements.
```
Example: 
if((i=2)==2) {…}
```
8.	Avoid compound conditional expressions – use Boolean variables to split parts into multiple manageable expressions.
```
Example: 
// Bad!
if (((value > _highScore) && (value != _highScore)) && (value <  _maxScore))
{…}

// Good!
isHighScore = (value >= _highScore);
isTiedHigh = (value == _highScore);
isValid = (value < _maxValue);
if ((isHighScore && ! isTiedHigh) && isValid)
{…}
```
9.	Avoid explicit Boolean tests in conditionals.
```
Example: 
// Bad!
if(IsValid == true)
{…};
// Good!
if(IsValid)
{…}
```
10.	Avoid writing multiple if conditions if you are sure that only one statement would succeed at anytime. Instead, use else if.
11.	Avoid multi-level if statements unless it is required.
12.	Only use switch/case statements for simple operations with parallel conditional logic.
13.	Prefer nested if/else over switch/case for short conditional sequences and complex conditions.
14.	Prefer polymorphism over switch/case to encapsulate and delegate complex operations.
15.	Avoid using Arraylists. Prefer using typed list wherever applicable.
16.	Prefer arrays to collections unless you need functionality. Use strongly typed arrays to avoid boxing and unboxing.
17.	Avoid accessing an Array via Property
18.	Optimize or avoid expensive operations in a loop.
19.	A lambda expression cannot directly capture a ref or out parameter from an enclosing method.
20.	A return statement in a lambda expression does not cause the enclosing method to return.
21.	A lambda expression cannot contain a goto statement, break statement, or continue statement whose target is outside the body or in the body of a contained anonymous function.

#Exceptions

This section provides how to use exception handling.

1.	Do not use try/catch blocks for flow-control.
2.	Only catch exceptions that you can handle.
3.	Never declare an empty catch block.
4.	Avoid nesting a try/catch within a catch block.
5.	Avoid re-throwing an exception. Allow it to bubble-up instead.
6.	If re-throwing an exception, preserve the original call stack by omitting the exception argument from the throw statement

```
Example: 
// Bad!
catch(Exception ex)
{
       Log(ex);
       throw ex;
}

// Good!
catch(Exception)
{
        Log(ex);
        throw;
}
```
7.	Only use the finally block to release resources from a try statement.
8.	Always use validation to avoid exceptions.

```
Example:

// Bad!
try
{
    conn.Close();
}
catch(Exception ex)
{
    // handle exception if already closed!
}

// Good!
if(conn.State != ConnectionState.Closed)
{
    conn.Close();
}
```
9.	Always set the innerException property on thrown exceptions so the exception chain & call stack are maintained.
10.	Avoid defining custom exception classes. Use existing exception classes instead.
11.	When a custom exception is required;
    - Always derive from Exception not ApplicationException.
    - Always suffix exception class names with the word “Exception”.
    - Always add the SerializableAttribute to exception classes.
    - Always implement the standard “Exception Constructor Pattern”:

```
public MyCustomException ();
public MyCustomException (string message);
public MyCustomException (string message, Exception innerException);
```
e.	Always implement the deserialization constructor:
`protected MyCustomException(SerializationInfo info, StreamingContext contxt);`

# Events, Delegates, Threading and Tasks

The following rules outline the design guidelines for implementing threading: 

1	Always check Event & Delegate instances for null before invoking.
2	Use the default EventHandler and EventArgs for most simple events.
3	Always derive a custom EventArgs class to provide additional data.
4	Use the existing CancelEventArgs class to allow the event subscriber to control events.
5	Always use the “lock” keyword instead of the Monitor type.
6	Only lock on a private or private static object.
`Example: lock(myVariable);`

7	Avoid locking on a Type.
`Example: lock(typeof(MyClass));`

8	Avoid locking on the current object instance.
`Example: lock(this);`
9	Static state must be thread safe. 
10	Always be aware of method calls in locked sections. Deadlocks can result when a static method in class A calls static methods in class B and vice versa. If A and B both synchronize their static methods, this will cause a deadlock. You might discover this deadlock only under heavy threading stress.
11	Do use tasks instead of ThreadPool work items. Tasks provide a variety of useful capabilities such as waiting, cancellation, and scheduling of continuations. If you use tasks in your program, having these capabilities available will make maintaining the code easier.

```
Task task = Task.Factory.StartNew(() =>
{
   double result = 0;
   for (int i = 0; i < 10000000; i++) result += Math.Sqrt(i);   
   Console.WriteLine(result);
});
```


12	Avoid creating threads directly, except if you need direct control over the lifetime of the thread.
13	Do use Task<T> types to represent asynchronously computed values. The task body delegate returns a value that is exposed via the Result property on the task. When you access the Result property, you will get the result immediately if the task has already completed, or otherwise the call will block until the computation completes.

```
Example :
Task<int> a = Task<int>.Factory.StartNew(() => { return Compute(0); }); 
Task<int> b = Task<int>.Factory.StartNew(() => { return Compute(1); }); 
Task<int> c = Task<int>.Factory.StartNew(() => { return Compute(2); });
 
int value = a.Result + b.Result + c.Result;
```
14	Avoid accessing loop iteration variables from the task body. More often than not, this will not do what you'd expect.
```
for (int i = 0; i < 5; i++)
{
   Task.Factory.StartNew(() => Console.WriteLine(i));
}
Console.ReadLine();
```
15	Do use a parallel loop instead of constructing many tasks in a loop. A parallel loop over N elements is typically cheaper than starting N independent tasks.
16	Avoid waiting on tasks while holding a lock. Waiting on a task while holding a lock can lead to a deadlock if the task itself attempts to take the same lock.
17	Do make sure that any static methods you implement are thread-safe. By convention, static methods should be thread-safe, and methods in BCL follow this convention. Carefully verify that your static methods will also work when called from multiple threads.
18	Do Not use objects on one thread after they got disposed by another thread. This mistake is easy to introduce when the responsibility for disposing an object is not clearly defined.
In the example below, one thread may call MoveNext() on an enumerator that has been disposed by another thread:

```
IEnumerator<int> e = ...; 
object myLock = new object(); 
Action walkEnumerator = () =>
{
   while (true)
   {
      lock (myLock)
      {
         if (!e.MoveNext()) break;  
      }
  } 
  lock (myLock) e.Dispose();
};

Parallel.Invoke(walkEnumerator, walkEnumerator);
```
19	Do use locks to protect shared state. Often, one lock per object provides the appropriate locking granularity.

```
Account account = new Account(1000); 
object accountLock = new object(); Parallel.Invoke(
   () => {
   lock(accountLock) { account.Withdraw(500); }
},

() => {
    lock(accountLock) { account.Withdraw(500); }
});
```
In this code sample, the two parallel Withdraw calls will be done with mutual exclusion, and so the account balance is not going to get corrupted.

20	Do always acquire locks in the same order. If two locks can be acquired in different order, deadlock may occur. This code contains a bug – if Transfer(A, B) and Transfer(B, A) are called concurrently, the program may deadlock:
```
static bool Transfer(Account a1, Account a2, int amount) 
{ 
   lock (a1) lock (a2) 
   { 
      bool success = a1.Withdraw(amount); 
      if (success) a2.Deposit(amount); 
      return success; 
   } 
}
```

21	Do not use publicly visible objects for locking. If an object is visible to the user, they may use it for their own locking protocol, despite the fact that such usage is not recommended.
22	Do not use Thread.Sleep() in your code. Consider alternatives like a Timer or ManualResetEvent or AutoResetEvent  

#Object Composition
1.	Always declare types explicitly within a namespace. Do not use the default “{global}” namespace.
2.	Avoid overuse of the public access modifier. Typically fewer than 10% of your types and members will be part of a public API, unless you are writing a class library.
3.	Consider using internal or private access modifiers for types and members unless you intend to support them as part of a public API.
4.	Never use the protected access modifier within sealed classes unless overriding a protected member of an inherited type.
5.	Avoid declaring methods with more than 5 parameters. Consider refactoring this code.
6.	Try to replace large parameter-sets (> than 5 parameters) with one or more class or struct parameters –especially when used in multiple method signatures.
7.	Do not use the “new” keyword on method and property declarations to hide members of a derived type.
8.	Only use the “base” keyword when invoking a base class constructor or base implementation within an override.
9.	Consider using method overloading instead of the params attribute (but be careful not to break CLS Compliance of your API’s).
10.	Always validate an enumeration variable or parameter value before consuming it. They may contain any value that the underlying Enum type (default int) supports.
```
Example:
public void Test(BookCategory cat)
    if (Enum.IsDefined(typeof(BookCategory), cat))
    {…}
}
```
11.	Consider overriding Equals() on a struct.
12.	Always override the Equality Operator (==) when overriding the Equals() method.
13.	Always override the String Implicit Operator when overriding the ToString() method.
14.	Call the base class's Dispose method if it implements IDisposable.
15.	Wrap instantiation of IDisposable objects with a “using” statement to ensure that Dispose() is automatically called.
```
Example:
using(SqlConnection cn = new SqlConnection(_connectionString))
{…}
```
16.	Always implement the IDisposable interface & pattern on classes referencing external resources.
```
Example: (shown with optional Finalizer)
public void Dispose()
{
    Dispose(true);
    GC.SuppressFinalize(this);
}
protected virtual void Dispose(bool disposing)
{
    if (disposing)
    {
        // Free other state (managed objects).
    }
    // Free your own state (unmanaged objects).
   // Set large fields to null.
}

// C# finalizer. (optional)
~Base()
{
    // Simply call Dispose(false).
    Dispose (false);
}
```
17.	Avoid implementing a Finalizer.
Never define a Finalize() method as a finalizer. Instead use the C# destructor syntax.

```
Example
// Good
~MyClass {…}
// Bad
void Finalize(){…}
```


18.	Use Domain Model pattern to model the business entity
19.	Map Business Objects to database tables using ORM
20.	Use auto properties instead of declaring local variables and properties on them 
## 3.2 Security Guidelines
1	Encrypt all the connection string values using .Net framework encryption
2	Encrypt all the Query string values if used. use URL routing to hide URL with query string parameters and consider scrambling
3	Enable ASP.Net request validation to indicate that the request validation should be triggered before any HTTP request data is accessed. This can be done with the following configuration changes in the web application configuration file.  Howerver you cannot rely on this for XSS Attacks. Refer https://owasp.org/www-community/ASP-NET_Request_Validation
`<httpRuntime requestValidationMode="4.5" targetFramework="4.8" />`

4	While using Entity to LINQ for database query: Avoid getting filter parameters from user input, if cannot be avoided then use stored procedure
5	OWASP(https://www.owasp.org/index.php/Main_Page) standards for web application security features is used by the Department. 
6	All guidelines given in OWASP must be considered and applied to the code base
## 3.3 Code Metrics Guidelines
1 Always ensure maintainability index is greater than 19.
2 Always ensure cyclometric complexity is not exceeding 15.
3 Avoid depth of inheritance going beyond 4
4 Avoid class coupling going beyond 30
5 Avoid Lines of code per method not exceeding 30.
## 3.4 Object Model Design
1.	Always prefer aggregation over inheritance.
2.	Avoid “Premature Generalization”. Create abstractions only when the intent is understood.
3.	Do the simplest thing that works, then refactor when necessary.
4.	Always make object-behavior transparent to API consumers.
5.	Avoid unexpected side-affects when properties, methods, and constructors are invoked.
6.	Always separate presentation layer from business logic.
7.	Always prefer interfaces over abstract classes.
8.	Only make members virtual if they are designed and tested for extensibility.
9.	Please use Microsoft best practice for class design(https://msdn.microsoft.com/en-us/library/czefa0ke(v=vs.71).aspx) for better class design.
10.	Adopt the KISS principle (Keep it short and simple - https://en.wikipedia.org/wiki/KISS_principle
11.	Adopt design patters where applicable that is fit for purpose.  There are no hard-set/black and white list that describes what patterns are available or applicable but a general guideline can be found here (https://en.wikipedia.org/wiki/Software_design_pattern and http://www.oodesign.com/)
12.	 Don’t break published interfaces. Design by contract.
13.	Reuse where possible and contribute if possible to the reuse repository (Common branch in TFS)
## 3.5 Loose Coupling
All projects use the Unity Framework for loose coupling between components, its interfaces and their clients.  Following are the guidelines that are expected to be adopted by projects:

1.	Do not declare the interface its implementation in the same project
2.	Clients that use Unity Framework for DI (Dependency Injection)  should not reference the project file. In this case, use the post build events during development purpose for debugging for copying DLLs into the debug folder
## 3.6 Caching 
We will be leveraging the NCache to read and write to a cache. Currently, an abstraction exists for caching that should be leveraged by all projects.
## 3.7 Data Access

1. The code will leverage the standard ODP.Net for database access operations.  
1. An abstraction exists as part of Web IOMS projects that should be leveraged by all projects. 
1. For SQL Server access, an abstraction exists as part of PPSSA that should be leveraged and extended (if at all) by projects.  

**Note**:
  
Inline SQL in C# is not allowed and should be in stored procedures invoked by the data access layer.  This applies for ALL SQL statements in both Oracle and SQL Server.
## 3.8 Exception Handling

We will be leveraging the Enterprise Library Exception Handling configuration, which has the following advantages over custom coding

- It allows exception handling policies to be defined and maintained at the administrative level so that policy makers, who might be system administrators as well as developers, can define how to handle exceptions. They can maintain and modify the rules that govern exception handling without changing the application block code. 
- It provides commonly used exception handling functions, such as the ability to log exception information, the ability to hide sensitive information by replacing the original exception with another exception, and the ability to maintain contextual information for an exception by wrapping the original exception inside another exception. These functions are encapsulated in .NET classes named exception handlers. 
- It can combine exception handlers to produce the desired response to an exception, such as logging exception information followed by replacing the original exception with another. 
- It invokes exception handlers in a consistent manner. This means that the handlers can be used in multiple places within and across applications.

Follow the guidelines given below.

1.  Add a new config section for Exception Handling which will be as follows:
  
```
<configSections>
    <section name="exceptionHandling" type="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration.ExceptionHandlingSettings, Microsoft.Practices.EnterpriseLibrary.ExceptionHandling, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" requirePermission="true" />
  </configSections>
```
ExceptionHandling section has the following sub-sections:
  
```
<exceptionHandling>
    <exceptionPolicies>
      <add name="AllExceptionsPolicy">
        <exceptionTypes>
          <add name="All Exceptions" type="System.Exception, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            postHandlingAction="None">
            <exceptionHandlers>
              <add name="Logging Exception Handler" type="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.LoggingExceptionHandler, Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
                logCategory="General" eventId="100" severity="Error" title="Enterprise Library Exception Handling"
                formatterType="Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.TextExceptionFormatter, Microsoft.Practices.EnterpriseLibrary.ExceptionHandling"
                priority="0" />
            </exceptionHandlers>
          </add>
        </exceptionTypes>
      </add>
    </exceptionPolicies>
  </exceptionHandling>
```
2.  Add references to exception handling related assemblies in the project using Add reference menu command. Assemblies which are required to be referenced are:
    - Microsoft.Practices.EnterpriseLibrary.Common.dll
    - Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.dll
    - Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.dll
    - Microsoft.Practices.EnterpriseLibrary.ServiceLocation.dll

3.  Then add the following namespaces at the top of the file:

`using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;`

In the code **always** catch the exception and handle the exception providing the appropriate policy. Following is a code snippet to just log the exception:

```
catch(Exception ex)
{
    bool rethrow = ExceptionPolicy.HandleException(ex, "Policy Name");
}
```
Use the following code snippet to rethrow the exception:
```
try
{
//Business Logic goes here, which should throw an exception
}
catch (Exception ex)
{
bool rethrow = ExceptionPolicy.HandleException(ex, "Policy Name");
      if (rethrow)
      {
        throw;
      }
}
```
**Note**:  List of exception classes that could be reused are already available to be consumed by projects. 

3.9 Enterprise Library

Logging will be implemented using latest version of Enterprise Library 5.0 (http://msdn.microsoft.com/en-us/library/ff632023.aspx). This section completely talks about the configuration of enterprise library 5.0 and typically we use Enterprise Library Configuration tool (EntLibConfig), which generates the elements based on the application block that we want to use it.

1.  All the log entries will be written to a custom logging database – ALTS schema
2.  Application configuration can be used with other Enterprise Library configuration as is.
3.  Adding a logging Configuration Section to the config files:

  
```
<configSections>
    <section name="loggingConfiguration" type="Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" requirePermission="true" />
  </configSections>
```
loggingConfiguration section has the following sub-sections:


```
<loggingConfiguration name="" tracingEnabled="true" defaultCategory="General">
    <listeners>
      <add name="Database Trace Listener" type="Microsoft.Practices.EnterpriseLibrary.Logging.Database.FormattedDatabaseTraceListener, Microsoft.Practices.EnterpriseLibrary.Logging.Database, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        listenerDataType="Microsoft.Practices.EnterpriseLibrary.Logging.Database.Configuration.FormattedDatabaseTraceListenerData, Microsoft.Practices.EnterpriseLibrary.Logging.Database, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        databaseInstanceName="Connection String" writeLogStoredProcName="WriteLog"
        addCategoryStoredProcName="AddCategory" formatter="Text Formatter" />
    </listeners>
    <formatters>
      <add type="Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter, Microsoft.Practices.EnterpriseLibrary.Logging, Version=5.0.414.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
        template="Timestamp: {timestamp}{newline}&#xA;Message: {message}{newline}&#xA;Category: {category}{newline}&#xA;Priority: {priority}{newline}&#xA;EventId: {eventid}{newline}&#xA;Severity: {severity}{newline}&#xA;Title:{title}{newline}&#xA;Machine: {localMachine}{newline}&#xA;App Domain: {localAppDomain}{newline}&#xA;ProcessId: {localProcessId}{newline}&#xA;Process Name: {localProcessName}{newline}&#xA;Thread Name: {threadName}{newline}&#xA;Win32 ThreadId:{win32ThreadId}{newline}&#xA;Extended Properties: {dictionary({key} - {value}{newline})}"
        name="Text Formatter" />
    </formatters>
    <categorySources>
      <add switchValue="All" name="General">
        <listeners>
          <add name="Database Trace Listener" />
        </listeners>
      </add>
    </categorySources>
    <specialSources>
      <allEvents switchValue="All" name="All Events" />
      <notProcessed switchValue="All" name="Unprocessed Category" />
      <errors switchValue="All" name="Logging Errors &amp; Warnings">
        <listeners>
          <add name="Database Trace Listener" />
        </listeners>
      </errors>
    </specialSources>
  </loggingConfiguration>
```

4.  Add references to logging related assemblies in the project using Add reference menu command. Assemblies which are required to be referenced are:
    - Microsoft.Practices.EnterpriseLibrary.Logging.dll
    - Microsoft.Practices.EnterpriseLibrary.Common.dll
    - Microsoft.Practices.ServiceLocation.dll
5.  Then add the following namespaces at the top of the file:

```
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
```
To log information use the following code where required:

```
LogEntry logEntry = new LogEntry();
logEntry.Priority = entryPriority;
logEntry.Categories.Add(“Specify Category”);
logEntry.Message = actualMessage;
logEntry.Title = entryTitle;

Logger.Write(logEntry);
```
**Note**: Web IOMS projects logs debug information in the ALTS schema AL_TRANSACTION_LOGGING table. The common code for logging into this table is available in the TFS for this purpose that is expected to be reused

**The following guidelines should be adopted:**

1. All the columns in the Transaction Logging should have meaningful information that is not ambiguous or duplicated
1. Care must be taken to ensure that logs are grammatically correct, and meaningful
1. Logging levels are significantly lower in production. So developers should ensure log statements have not just DEBUG statements but also INFO, WARN etc that are useful for support personnel with BAU
## 3.9 WCF
# Service Design

## Tenets of service design

It is not possible to achieve the higher levels of productivity needs and requirement changes that happen in IT projects today without SOA. SOA enables us to compose applications using pre-existing services. However to achieve the “application composition” approach, we need to build a set of services that follow the tenets of SOA as briefed below.

### Tenet 1: Boundaries must be explicit

- A well-defined and published public interface must be the main entry point into the service, and all interactions occur using this public interface.
- It should be easy for other developers to consume the service. Also, the service interface should allow the ability to evolve over time without breaking existing consumers of the service.
- Avoid RPC style interactions and instead use document style interactions based on explicit business messages.
- Provide for fewer course-grained interfaces as opposed to many fine-grained interfaces.
- Implementation details should be kept internal in order to avoid tight coupling between the consumer and the service.

### Tenet 2: Services must be autonomous

- Service versioning and deployment must be independent of the consuming applications
- Contracts once published should never be changed, except for additions

Tenet 3: Services can only share the schema and contract, not the implementation class

- Service contracts constituting the data, interface and policy do not change and remain stable
- Contracts must be clear and explicit and be named so that it is clear about their intent and use
- If there are breaking changes to the contract, then version the new contract so as to minimize changes to existing service consumers
- Never expose the internal data representation, the only representation will be the publicly available schema

### Tenet 4: Service compatibility must be based on policy

- It will not be possible to understand everything about the service from the interface alone (WSDL for SOAP services)
- Compatibility aspects need to be specified in policies
- Policies will have to be explicit so that the consumer is aware of the invocation criteria for the services
- Policies need to cover both service expectations and semantic compatibilities

## General

1. Place service code in a class library and not in any hosting EXE.
1. Do not provide parameterized constructors to a service class unless it is a singleton that is hosted explicitly.
1. Enable reliability in the relevant bindings.
1. Provide a meaningful namespace for both service contracts and data contracts. For outward-facing services use your company’s URL or equivalent URN with a year and month to support versioning. For intranet, use meaningful unique name.
1. Always close or dispose of the proxy	
1. Use channel factory for connecting the services which enables to centralize the service invocation

## Service Contract

1.	Always apply the ServiceContractAttribute on an interface, not a class
```
//Avoid 
[ServiceContract] 
class MyService
{
  [OperationContract]
  public void MyMethod()
  {...}
}
//Correct 
[ServiceContract] 
interface IMyContract
{
   [OperationContract]
   void MyMethod();
}
class MyService : IMyContract
{
  public void MyMethod()
  {...}
}
```
2.	Prefix the service contract name with I
3.	Avoid property like operations

```
//Avoid 
[ServiceContract] 
interface IMyContract
{
   [OperationContract]
   string GetName();

   [OperationContract]
   void SetName(string name);
}
```
4.	Avoid contracts with one member
5.	Do not have more than twenty members per service contract. Twelve is probably the practical limit
6.	Always ensure service contract versioning 

## Data Contract

1. Avoid inferred data contracts. Always be explicit and apply the DataContract attribute.
1. Use  the  DataMemberAttribute on  properties  or  read-only  public  members only.
1. Avoid explicit XML serialization on your own types.
1. When using the Order property, assign the same value to all members coming from the same level in the class hierarchy.
1. Support IExtensibleDataObject on your data contracts. Use explicit interface implementation.
1. Avoid  setting  IgnoreExtensionDataObject on  the  ServiceBehavior
1. and CallbackBehavior attributes to true. Keep the default of false.
1. Do not mark delegates and events as data members.
1. Do not pass .NET specific types such as Type as operation parameters.
1. Do not accept or return ADO.NET DataSet and DataTable (or their type-safe subclasses) from operations. Return a neutral representation such as an array.
1. Suppress the generation of a generic type parameter hash code and provide a legible type name instead.
1. Share data contract across projects in a solution when possible.
1. Always ensure data contract and message versioning
1. Always ensure data cleansing by validating input parameters to services. Use the Code Contract API available with the Base Class library for this purpose

## Instance Management

1. Prefer the per-call instance mode when scalability is a concern.
1. Do prefer durable services configuration to explicit per-call configuration.
1. If selecting SessionMode.NotAllowed on the contract, always configure the service instancing to InstanceContextMode.PerCall.
1. Do not mix sessionful contracts and session-less contracts on the same service.
1. Avoid a singleton unless you have a natural singleton.
1. Use ordered delivery with a sessionful service.
1. Avoid instance deactivation with a sessionful service.
1. Avoid demarcating operations.
1. With durable services, always designate a completing operation.

## Operations and Calls

1. Do not treat one-way calls as asynchronous calls.
2. Do not treat one-way calls as concurrent calls.
3. Expect exceptions out of a one-way operation.
4. Enable reliability even on one-way calls. Use of ordered delivery is optional for one- way calls.
5. Avoid one-way operations on a sessionful contract. If used, make it the terminating operation:

```
[ServiceContract(SessionMode = SessionMode.Required)]
interface IMyContract
{
[OperationContract]
void MyMethod1();

[OperationContract(IsOneWay = true,IsTerminating = true)]
void MyMethod2();
}
```
6.  Name the callback contract on the service side after the service contract suffixed by Callback:

```
interface IMyContractCallback
{...}
[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
interface IMyContract
{...}
```


7. Strive to mark callback operations as one-way.
8. Use callback contracts for callbacks only.
9. Avoid mixing regular callbacks and events on the same callback contract.
10. Event operations should be well designed
`void return type` 
11. No out parameters
12.  Marked as one-way operations
13. Always provide explicit methods for callback set-up and teardown:

```
[ServiceContract(CallbackContract = typeof(IMyContractCallback))]
interface IMyContract
{
[OperationContract]
void DoSomething();

[OperationContract]
void Connect();

[OperationContract]
void Disconnect();
}
interface IMyContractCallback
{...}
```

## Faults

1.  Never use a proxy instance after an exception even if you catch that exception.
2.  Avoid fault contracts and allow WCF to mask the error.
3.  Do not reuse the callback channel after an exception even if you catch that exception as the channel may be faulted.
4.  Use the FaultContractAttribute with exception classes as opposed to mere serializable types:
```
//Avoid 
[OperationContract] [FaultContract(typeof(double))]
double Divide(double number1,double number2);

//Correct
[OperationContract] [FaultContract(typeof(DivideByZeroException))] double Divide(double number1,double number2);
```
5.  Avoid	lengthy	processing	such	as	logging	in IErrorHandler.ProvideFault().
6.  With both	service	classes	and callback classes set IncludeExceptionDetailInFaults to true in debug sessions, either in the config file or programmatically:

```
public class DebugHelper
{
   public const bool IncludeExceptionDetailInFaults =
#if DEBUG
   true;
 
#else
   false
#endif
}
 
[ServiceBehavior(IncludeExceptionDetailInFaults =     
          DebugHelper.IncludeExceptionDetailInFaults)]
class MyService : IMyContract
{...}
```
**Note**: Common reusable library exists for handling service exceptions in TFS that should be reused by projects

## Transactions

1.  Never manage transactions directly.
2.  Apply TransactionFlowAttribute on the contract not the service class.
3.  Do not perform transactional work in the service constructor.
4.  Configure services for either Client or Client/Service transactions. Avoid None or Service transactions.
5.  On the client always catch all exceptions thrown by a service configured for None or Service transactions.
6.  Enable reliability and ordered delivery even when using transactions.
7.  In a service operation, never catch an exception and manually abort the transaction:

```
//Avoid:
[OperationBehavior(TransactionScopeRequired = true)]
public void MyMethod()
{
  try
  {
      ...
  }
  catch
  {
      Transaction.Current.Rollback();
  }
}
```
8.  Never manage transactions directly.
9.  If you catch an exception in a transactional operation, always re-throw it or another exception.
10.  Keep transactions short.
11.  Always use the default isolation level of IsolationLevel.Serializable.
12.  Do not call one-way operations from within a transaction.
13.  Do not call non-transactional services from within a transaction.
14.  Do not access non-transactional resources (such as the file system) from within a transaction.
15.  With a sessionful service, avoid equating the session boundary with the transaction boundary by relying on auto complete on session close.
16.  With transactional durable services, always propagate the transaction to the store by setting SaveStateInOperationTransaction to true.

## Concurrency Management

1.  Always provide thread-safe access to:
    -  Service in-memory state with sessionful or singleton services. 
    -  Client in-memory state during callbacks.
    -  Shared resources such as files. 
    -  Static variables.
2.  Prefer ConcurrencyMode.Single (the default). It enables transactional access, and it is thread-safe without any effort.
3.  Keep operations on single-mode sessionful and singleton services short in order to avoid blocking other clients for long.
4.  With ConcurrencyMode.Multiple you must use transaction auto-completion.
5.  Consider  using  ConcurrencyMode.Multiple on  per-call  services  to  allow concurrent calls.
6.  Transactional singleton services with ConcurrencyMode.Multiple must have ReleaseServiceInstanceOnTransactionComplete set to false:
 
```
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,  
                  ConcurrencyMode = ConcurrencyMode.Multiple,          
                  ReleaseServiceInstanceOnTransactionComplete = false)]
class MySingleton : IMyContract
{...}
```
7.  Never self-host on a UI thread, and have the UI application call the service.
8.  Never allow callbacks to the UI application that called the service unless the callback posts the call using SynchronizationContext.Post().
9.  When supplying the proxy with both synchronous and asynchronous methods apply the FaultContractAttribute only to synchronous methods.
10.  Keep asynchronous operations short. Do not equate asynchronous calls with lengthy operations.
11.  Do not mix transactions with asynchronous calls.

## Role based security

Always implement role based security for the services methods.  A common reusable library already exists in TFS for this purpose that could potentially be extended.

## 3.10 HTML 
# Use Correct Document Type
Page should always declare the document type as the first line in the HTML document:
`<!DOCTYPE html>`
# Use Lower Case Element Names
HTML5 allows mixing uppercase and lowercase letters in element names. But it is recommended using lowercase element names:
•	Mixing uppercase and lowercase names is bad
•	Developers are used to use lowercase names (as in XHTML)
•	Lowercase look cleaner
•	Lowercase are easier to write

```
<div> 
  <p>This is a paragraph.</p>
</div>
```

# Close All HTML Elements
In HTML5, it is not necessary to close all elements (for example the <p> element). But it is recommend closing all HTML elements

```
<div> 
  <p>This is a paragraph.</p>
</div>
```

# Close Empty HTML Elements
In HTML5, it is optional to close empty elements. But it is recommended to close all html element tags.

`<meta charset="utf-8" />`
# Use Lower Case Attribute Names
HTML5 allows mixing uppercase and lowercase letters in attribute names.  It is recommended using lowercase attribute names:
•	Mixing uppercase and lowercase names is bad
•	Developers are used to use lowercase names (as in XHTML)
•	Lowercase look cleaner
•	Lowercase are easier to write

`<div class="formElement">`

# Quote Attribute Values
HTML5 allows attribute values without quotes.
It is recommend quoting attribute values:
•	You have to use quotes if the value contains spaces
•	Mixing styles is never good
•	Quoted values are easier to read

`<table class="table striped">`

# Image Attributes
Always use the alt attribute with images. It is important when the image cannot be viewed.

`<img src="photo.gif" alt="Photo”/>` 

# Omitting <html> and <body>
In the HTML5 standard, the <html> tag and the <body> tag can be omitted.  But, it is not recommended omitting the <html> and <body> tags. The <html> element is the document root. It is the recommended place for specifying the page language.  Omitting <html> or <body> can crash DOM and XML software. Omitting <body> can produce errors in older browsers.

# Meta Data
The <title> element is required in HTML5. Make the title as meaningful as possible


```
<!DOCTYPE html>
<head>
  <meta charset="UTF-8">
  <title>My Page</title>
</head>
```

# HTML Comments
Short comments should be written on one line, with a space after <!-- and a space before -->
`<!-- This is a comment -->`

# Tidying up HTML tags with indentation
It will be hard to look at the HTML in the editor and to understand the code if it is not indented properly. To help to see the structure of a page's HTML, it is best to indent the code.


```
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link rel="icon" type="image/png" href="UIM/Fav_icon.png">
    <title>@ViewBag.Title</title>

    @Styles.Render("~/Content/css")
    <script type="text/javascript">
        var getVirtualDirectory = "@Url.Content("~")";
    </script>
</head>
```

# Use proper Semantic HTML Markup
It has been recognized as a practice of using element types properly  their meaning to mark up content in the document. The key motive of using semantic coding is to make the website code structure user-friendly for readers. Readers can be common human beings, web browsers, or platforms.
Key benefits of semantic html:
•	User-friendliness
•	Better Accessibility
•	Lighter Code Structure

```
<header>…</header>
<footer>…</footer>
```

## 3.11 ASP.NET and MVC 
# ASP.NET

1.	Do not use session variables throughout the code. Use session variables only within the classes and expose methods to access the value stored in the session variables. A class can access the session using System.Web.HttpContext.Current.Session
2.	Do not store large objects in session. Storing large objects in session may consume lot of server memory depending on the number of users.
3.	Always use style sheet to control the look and feel of the pages. Never specify font name and font size in any of the pages. Use appropriate style class. This will help you to change the UI of your application easily in future. Also, if you like to support customizing the UI for each customer, it is just a matter of developing another style sheet for them
4.	Use client-side validations avoid server-side validations
5.	Apply 'using' statement to dispose resources
6.	Grid view should have maximum 20 records in a single view, rest of the records will be handled through paging. Server-side paging should be the preferred option.
7.	Enable minification and bundling of the JavaScript and CSS files
8.	Enable compression in the IIS for optimal performance of the application

# MVC
## Model recommendations
1.	It is highly recommended to use existing BPL entities as models
2.	If there any new properties are required, inherit the existing BPL entities and extend it appropriately.
3.	If the existing BPL entities scattered across multiple classes then create the new model class, this should be least priority option
## Session recommendations
1.	Avoid using session to have a larger data in to it.
2.	Do not overuse session object, if any information requires only across a redirect, then  use temp variables using TempData dictionary 
## Controller recommendations
1.	All logical entities or groups must be maintained in separate controller, if require use partial classes to split the same entity controller into multiple files for readability
2.	Do not put HTML in the controller
3.	Do use client-side model binding
4.	Do use Post when submitting the forms
5.	HTTP  GET – It is used for idempotent data(non-changing data) to the model, such as lookup data, grid data and any static data  
6.	HTTP POST – It is used to changing data to the model, that actions to change the database ie., CUD operations.
7.	Use RAZOR engine tags only when the same could not be achieved by the client-side kendo binding
8.	Try to use model binding instead of manually parsing the request
9.	Avoid using class level variables
10.	Controller should not expose any public methods other than Action methods or HTTP GET or HTTP POST methods
11.	Ensure all the unused code blocks are removed from the controller 
12.	Ensure all the unused parameters are removed from the action methods, HTTP GET, HTTP POST and private methods
13.	Do not re-throw exceptions unless it is required
14.	Use appropriate http action methods for making any ajax call to the controller. Refer the following table for more details.
	-  **GET** - The GET requests a data from a specified resource.  GET requests should be used to only retrieve the data and should not have other effect. 
	-  **POST** - The POST method submits data to be processed to s specified resource. The POST method requests the server to accept the entity enclosed with the request. 
	-  **PUT** - The PUT method uploads the entity to be stored under the supplied URI. If the URI represents an existing resource, it is modified; if not the server can create the resource.
	-  **DELETE** - The delete method deletes the specified resource
	-  **OPTIONS** - The OPTIONS method returns the HTTP methods that are supported by the server for the specified URL.
	-  **CONNECT** - The CONNECT method converts the request connection to a transparent TCP/IP tunnel
	-  **HEAD** - The HEAD method is same as GET, but returns only HTTP headers and no document body 
	-  **TRACE** - The TRACE method echoes the received request to the client so that client can see what changes or additions have been made by any intermediate servers
	-  **PATCH** - The PARTIAL method applies partial modifications to a particular resource
## Error handling 
1.	Handle unexpected errors at the central location. 
2.	Use ASP.Net MVC “HandleError” attribute on an actions and/or controller to handle the uncaught exceptions
## View recommendations 
1.	Consider view as a presentation of model
2.	Do put HTML in views and partial views, not in controller
3.	Avoid creating the copy of the same HTML into multiple, it is better handle such scenarios using partial views
## Routing recommendations 
1.	Create routes from specific to general 
2.	Use named route mechanism to avoid route ambiguity
## Filters
1.	Use filters to add behaviours to action method.
2.	To define common behaviour patterns use filters on the base controller class and then ensure that other controllers derive from the base class.
	Note: Security Filter for validating permissions is already available and expected to be reused across projects.
## View Bag recommendations
1.	View Bag, TempData and/or ViewData should be used sparingly and for the purpose of returning simple data types only or avoided if possible. 
2.	ViewBag should only be used to pass information to the layout of your view
3.	Returning large and/or complex data types should adopt
	-  Strongly typed ViewModels
	-  HttpGet/HttpPost methods via JSON/AJAX requests


# 3.12 ADO.NET 
For best practices and guidelines with ADO.NET, refer to the link [https://msdn.microsoft.com/en-us/library/ms998569.aspx](https://msdn.microsoft.com/en-us/library/ms998569.aspx)










