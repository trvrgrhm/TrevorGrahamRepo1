class UserInputClass {


    reminder:any;
    dimes:number = 0;
    nickels: number = 0;
    penny:number = 0;
   
    constructor(reminder:any){
        this.reminder = reminder;
    }

    NameLess()
    {
        while (this.reminder > 0) {
            // console.log(reminder);
            if ( this.reminder >= .10 )
            {
                //do something
                this.dimes++;
                this.reminder -= .10;
                this.reminder = this.reminder.toFixed(2)
                // console.log("enter the dimes stuff: ", dimes);
                
            }
            else if ( this.reminder >= .05)
            {
                //do something   
                this.nickels++;
                this.reminder -= .05;
                this.reminder = this.reminder.toFixed(2)
                // console.log("enter the nickels stuff: ", nickels);
            }
            else // if (reminder >= .01)
            {
                this.penny++;
                this.reminder -= .01;
                this.reminder = this.reminder.toFixed(2)
                // console.log("enter the penny stuff: ", penny);
            }
    
        }
        
    }

    DomManipulation(){
        //Saving into the DOM stuff
        let myContainer = document.querySelector("#container");
    
        document.querySelectorAll("#container p")
            .forEach( (child)=> child.remove() );
    
        let dimetag = document.createElement('p');
        let nickeltag = document.createElement('p');
        let pennytag = document.createElement('p');
    
        dimetag.textContent = "Dimes: " + this.dimes;
        nickeltag.textContent = "Nickels: " + this.nickels;
        pennytag.textContent = "Pennys: " + this.penny;
    
        myContainer?.appendChild(dimetag);
        myContainer?.appendChild(nickeltag);
        myContainer?.appendChild(pennytag);
        // myDimeTag?.textContent = "My value"
    }


}


function OutsideFunc(){
    
    let userInput: any = document.querySelector("#txtNumber");
    let reminder :any = userInput.value;
    
    let myClass = new UserInputClass(reminder);
    myClass.NameLess();
    myClass.DomManipulation();

}

