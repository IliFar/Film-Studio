const form = document.getElementById("form");

form.addEventListener("submit", (e) => {
  e.preventDefault();
  const formData = new FormData(form);

  fetch("api/user/signup", {
    method: "post",
    body: JSON.stringify(formData),
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((res) => res.json)
    .then((data) => {
      console.log(data);
    });
});
