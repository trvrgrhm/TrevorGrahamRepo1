function BasicFunction()
{
    alert("Hero Js Warudo")
}

function ParentFunction(callback){
    console.log("In parent function:");
    let name = prompt('Please enter your name: ');
    callback(name);
    console.log("Back in parent function")
}
function Callback(name)
{
    console.log('Hello '+name);
    console.log('calling back');
}
function Callback2()
{
    console.log('in another function');
    console.log('calling back');
}
//this IIFE uses the outer function's count parameter
let Outer = (() =>
{
    let count = 0;
    return function inner(){
        return count+=1;
    };

})();

let addMore=()=>{
    let count = 0;
    return () => {
        count+=1;
        return count;
    };
};
// using function as an instance
let add = addMore();
let addAgain = addMore();
