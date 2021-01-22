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
  
  let ps = document.getElementsByTagName('p');
  
fetch('https://jsonplaceholder.typicode.com/posts', obj)
  .then((response) => response.json(),err=>alert('there was an error'))
  .then(res1=>{
      ps[0].innerHTML = `POST: Dude #${res1.userId} goes by the name of ${res1.title} ${res1.body}`
  });



  fetch('https://jsonplaceholder.typicode.com/posts/1')
    .then(response=>response.json(),error=>alert('There was an error'))
    .then(json =>{ps[1].innerHTML =`GET: title: ${json.title} `});

    let obj2 ={
        method: 'PUT',
        body: JSON.stringify({
          title: 'foo',
          body: 'bar',
          userId: 1,
        }),
        headers: {
          'Content-type': 'application/json; charset=UTF-8',
        },
      }

      fetch('https://jsonplaceholder.typicode.com/posts/1', obj2)
      .then(response=>response.json(),error=>alert('There was an error'))
      .then(json =>{ps[2].innerHTML =`PUT: title: ${json.title} `});


fetch('https://jsonplaceholder.typicode.com/posts/1',{method:'DELETE'})
.then(response=>console.log(response));
  