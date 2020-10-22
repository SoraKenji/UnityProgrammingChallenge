# UnityProgrammingChallenge

This is a Unity project that reads a JSON file and shows a table on screen with the JSON data using Unity UI components. It's a challenge proposed by Giant Monkey Robot and developed by Claudio Torres in order to apply as a Unity developer.

JSON File has this format. It can vary in headers' names and quantity. It can also vary in rows' quantity (quantity of elements of Data array). But this code is implemented so it can work with any number of column headers and data array elements.

If JSON file has discrepancies between headers' quantity/names and "Data" array elements, then an error message will come out.

{
    "Title": "Team Members",
    "ColumnHeaders" :
    [
        "ID",
        "Name",
        "Role",
        "Nickname"
    ],
    "Data" :
    [
    {
        "ID" : "001",
        "Name" : "John Doe",
        "Role" : "Engineer",
        "Nickname" : "KillerJo"
    },
    {
        "ID" : "023",
        "Name" : "Claire Dawn",
        "Role" : "Engineer",
        "Nickname" : "Claw"
    },
    {
        "ID" : "012",
        "Name" : "Paul Beef",
        "Role" : "Designer",
        "Nickname" : "BeefyPaul"
    },
    {
        "ID" : "056",
        "Name" : "Sally Sue",
        "Role" : "Artist",
        "Nickname" : "Sue5555"
    },
    {
        "ID" : "0561",
        "Name" : "Sally Sue1",
        "Role" : "Artist1",
        "Nickname" : "Sue55551"
    }
    ]
}
