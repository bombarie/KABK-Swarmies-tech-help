/**
 * Created by adriaanwormgoor on 14/05/2021.
 */


let $users;
let $scr1, $scr2, $fname, fname = null;
let screens = [];

function getUsers() {
  // console.log("f:getUsers");

  $.ajax({
    url: "/users/list",
    type: "GET",
    success: function(res) {
      // console.log("got response from server >> data: ", res.data);
      if (res.result === "success" ) {
        $list = $("#list");
        $list.empty();
        $list.append("named users: " + "<br/>");
        for (var p in res.data) {
          if (typeof res.data[p].name == "string") {
            $list.append("<li>" + res.data[p].name + " (id: " + res.data[p].id + ")</li>");
          }
        }
        $list.append("<br/><br/>");
        $list.append("unnamed users: " + "<br/>");
        for (var p in res.data) {
          if (!res.data[p].hasOwnProperty("name")) {
            $list.append("<li>id: " + res.data[p].id + "</li>");
          }
        }
        // console.log("res.data: " , res.data);
        // if (typeof callback === "function") {
        //   callback(res.data);
        // }
      } else {
        console.warn("something went wrong getting the list of Midi devices names");
      }
    },
    dataType: "json"
  });
}

let getUsersInterval;

$(document).ready(function() {
  console.log("ready");

  $users = $("#users");

  getUsersInterval = setInterval(getUsers, 1000);

});
