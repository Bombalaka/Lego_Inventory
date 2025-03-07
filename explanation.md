## data folder and respositpry what do they do ? 
The constructor tries to connect to MongoDB using settings from configuration.
If there’s any error (MongoDB down or connection issues), it sets _useInMemory to true and uses a simple list.
Both methods (GetAllAsync and AddAsync) decide which storage to use based on the _useInMemory flag.


## Explanation to lego controller


The Index action gets all LEGO sets from our repository and passes them to the view.
The Create actions show a form (GET) and then process the form submission (POST).