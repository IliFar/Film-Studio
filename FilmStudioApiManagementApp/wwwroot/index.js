
const link = document.getElementById("film");

link.addEventListener("click", () => {
  fetch("api/films")
    .then((res) => res.json())
    .then((data) => {
      console.log(data);
    });
});
