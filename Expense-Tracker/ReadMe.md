 CLI App
 N-Tier Architecture
 
 The application should allow users to add, delete, and view their expenses. The application should also provide a summary of the expenses.

 The list of commands and their expected output is shown below:

bash

$ expense-tracker add --description "Lunch" --amount 20
$ expense-tracker add --description "Dinner" --amount 10
$ expense-tracker list
$ expense-tracker summary
$ expense-tracker delete --id 2
$ expense-tracker summary
$ expense-tracker summary --month 8
