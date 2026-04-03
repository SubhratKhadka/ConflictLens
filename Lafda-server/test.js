const TOKEN = "f9fa7db268a116bb";

const url = "https://ucdpapi.pcr.uu.se/api/nonstate/25.1?pagesize=100&Country=630";

const params = new URLSearchParams({
  pagesize: 100,
  page: 1,
});

async function main() {
  try {
    const response = await fetch(`${url}?${params.toString()}`, {
      headers: {
        "x-ucdp-access-token": TOKEN,
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();
    console.log(data)

    console.log(`Total events: ${data.TotalCount}`);

    data.Result.forEach((event) => {
      console.log(event.id, event.country);
    });
  } catch (err) {
    console.error("Error:", err.message);
  }
}

main();