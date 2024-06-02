document.addEventListener('DOMContentLoaded', function () {
  // Ovaj kod će se izvršiti kada se HTML potpuno učita
  var stanje=false;
  // Dodajte JavaScript kod za praćenje klikova na dokumentu
  document.addEventListener('click', function (event) {
    if(!stanje){
      stanje=true
    }
    else{
        //console.log("Klik");
        var overlappingRows = document.getElementById("overlapping-rows");
        if (overlappingRows) {
            overlappingRows.style.display = 'none'; // Sakrijte preklapanje
        }
    }
  });


    document.getElementById("form1").addEventListener('click' ,function(){
      stanje=false;
        //console.log("Uslo sma u myscript.js");
        var overlappingRows = document.getElementById("overlapping-rows");
      if (overlappingRows) {
        overlappingRows.style.display = 'block';
      }

    })


  // Dodajte JavaScript kod za praćenje skrolovanja
  window.addEventListener('scroll', function () {
    //console.log("scroll");

    var overlappingRows = document.getElementById("overlapping-rows");
    if (overlappingRows) {
      overlappingRows.style.display = 'none'; // Sakrijte preklapanje kada korisnik skrola
    }
  });




  // Postavite event listener nakon što se HTML potpuno učita
  // var overlappingRowsElement = document.getElementById("overlapping-rows");
  // if (overlappingRowsElement) {
  //   console.log("usli smo u nesto");
  //   overlappingRowsElement.addEventListener('click', function () {
  //     var overlappingRows = document.getElementById("overlapping-rows");
  //     if (overlappingRows) {
  //       overlappingRows.style.display = 'block'; // Vrati preklapanje
  //     }
  //   });
  // }
});
