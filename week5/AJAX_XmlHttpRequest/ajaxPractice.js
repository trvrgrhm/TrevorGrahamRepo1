// https://jsonplaceholder.typicode.com/guide/

let obj ={
    method: 'POST',
    body: JSON.stringify({
      title: 'foo',
      body: 'bar',
      userId: 1,
    }),
    headers: {
      'Content-type': 'application/json; charset=UTF-8',
    },
  }
  
fetch('https://jsonplaceholder.typicode.com/posts', obj)
  .then((response) => response.json(),err=>alert('there was an error'))
  .then(res1=>{
      let ps = document.getElementsByTagName('p');
      ps[0].innerHTML = `Dude #${res1.userId} goes by the name of ${res1.title} ${res1.body}`
  });

//fetch------------------------

// let pArray = document.getElementsByTagName("p");

// for(let i=0;i<pArray.length;i++)
// {
//     fetch('http://api.icndb.com/jokes/random')
//     .then(response=>response.json(),error=>alert('There was an error'))
//     .then(json =>{pArray[i].innerHTML = json.value.joke} )
//     .finally(alert("finally!"))
// }

// fetch('http://api.icndb.com/jokes/random')
// .then(response=>response.json())
// .then(response1 => pArray[0].innerHTML = response1)







// let xhr = new XMLHttpRequest();

// let pArray = document.getElementsByTagName("p");


//     xhr.open('GET','http://api.icndb.com/jokes/random',true);
//     // xhr.onload = chuckNoris(i,xhr)
//     xhr.send();
//     // xhr.onload = chuckNoris(2,xhr);
//     xhr.onload = function(){pArray[2].innerHTML = xhr.responseText;}

// function chuckNoris(i){
//     // let pArray = document.getElementsByTagName("p");
//     pArray[i].innerHTML = xhr.responseText;
// }
// xhr = new XMLHttpRequest();

// let pArray = document.getElementsByTagName("p");


    // xhr.open('GET','http://api.icndb.com/jokes/random',true);
    // // xhr.onload = chuckNoris(i,xhr)
    // xhr.send();
    // xhr.onload = chuckNoris(3,xhr.responseText);




//setup here



// function chuckNoris(i,xhr){
//         // let pArray = document.getElementsByTagName("p");
//         // for(let i = 0;i<pArray.length;i++){
//             pArray[i].innerHTML = xhr.text;
//         // }
//         // pArray.forEach(element => {
//         //     element.innerHTML = xhr.responseText;
//         // });
//         // pArray[0].innerHTML = xhr.responseText;
// }

// xhr.open('GET','http://api.icndb.com/jokes/random',true);
// xhr.send();


    // xhr.onreadystatechange = function(){
    // pArray[0] = xhr.responseText;
    // } 
    // // chuckNoris(pArray[i],xhr.responseText)
    // xhr.open('GET','http://api.icndb.com/jokes/random',true);
    // xhr.send();

// for(let i = 0;i<pArray.length;i++){
//     let xhr = new XMLHttpRequest();
//     // chuckNoris(pArray[i],xhr.responseText)
//     xhr.open('GET','http://api.icndb.com/jokes/random',true);
//     xhr.onload = chuckNoris(i,xhr)
//     xhr.send();
// }

// Array.prototype.forEach.call(pArray,x => {
//     xhr.open('GET','http://api.icndb.com/jokes/random',true);
//     xhr.send();
//     xhr.onload() = chuckNoris(x,xhr.responseText)
//     // pArray[x].innerHTML = xhr.responseText;
//     // xhr.onreadystatechange = chuckNoris(x);
//     // x.innerHtml = function (){
//     //     if(this.readyState == 4 && this.status == 200){
//     //         xhr.responseText;
//     //     }
//     // }
// });