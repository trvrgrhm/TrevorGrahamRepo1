function GetDigimon(){
    let digiName = document.querySelector('#digimonInput').value;
    fetch(`https://digimon-api.vercel.app/api/digimon/name/${digiName}`)
    //data is sent in an array of size one
    .then(result => result.json()).then(data=>processResult(data[0]))

    function processResult(data){
        document.querySelector('.digimonResult img').setAttribute('src',data.img);
        document.querySelector('.digimonResult caption').foreach((element)=>element.remove())
        let caption = document.createElement('caption');
        let digiName = document.createTextNode(data.name);
        caption.appendChild(digiName);
        document.querySelector('.digimonResult').appendChild(caption);
        document.querySelector('#digimonInput').valu = '';
    }
}