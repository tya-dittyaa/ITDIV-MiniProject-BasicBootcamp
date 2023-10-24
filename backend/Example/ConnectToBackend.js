async function logUsers() {
  const response = await fetch("http://localhost:5069/api/Category", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
    },
  });

  console.log(await response.json());
}

logUsers();
