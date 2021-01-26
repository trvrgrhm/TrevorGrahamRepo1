"use strict";
var UserInputClass = /** @class */ (function () {
    function UserInputClass(reminder) {
        this.dimes = 0;
        this.nickels = 0;
        this.penny = 0;
        this.reminder = reminder;
    }
    UserInputClass.prototype.NameLess = function () {
        while (this.reminder > 0) {
            // console.log(reminder);
            if (this.reminder >= .10) {
                //do something
                this.dimes++;
                this.reminder -= .10;
                this.reminder = this.reminder.toFixed(2);
                // console.log("enter the dimes stuff: ", dimes);
            }
            else if (this.reminder >= .05) {
                //do something   
                this.nickels++;
                this.reminder -= .05;
                this.reminder = this.reminder.toFixed(2);
                // console.log("enter the nickels stuff: ", nickels);
            }
            else // if (reminder >= .01)
             {
                this.penny++;
                this.reminder -= .01;
                this.reminder = this.reminder.toFixed(2);
                // console.log("enter the penny stuff: ", penny);
            }
        }
    };
    UserInputClass.prototype.DomManipulation = function () {
        //Saving into the DOM stuff
        var myContainer = document.querySelector("#container");
        document.querySelectorAll("#container p")
            .forEach(function (child) { return child.remove(); });
        var dimetag = document.createElement('p');
        var nickeltag = document.createElement('p');
        var pennytag = document.createElement('p');
        dimetag.textContent = "Dimes: " + this.dimes;
        nickeltag.textContent = "Nickels: " + this.nickels;
        pennytag.textContent = "Pennys: " + this.penny;
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(dimetag);
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(nickeltag);
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(pennytag);
        // myDimeTag?.textContent = "My value"
    };
    return UserInputClass;
}());
function OutsideFunc() {
    var userInput = document.querySelector("#txtNumber");
    var reminder = userInput.value;
    var myClass = new UserInputClass(reminder);
    myClass.NameLess();
    myClass.DomManipulation();
}
