import React, { useEffect, useRef, useState } from "react";
import PropertyCard from "../components/Property-card.tsx";
import { useNavigate } from "react-router-dom";

const ViewProperty = () => {
  const navigate = useNavigate()
  const [properties, setProperties] = useState([]);
  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");
 
 
  useEffect(() => {
    var res = localStorage.getItem("loginData")
          if(res){

            if(JSON.parse(res)?.email && JSON.parse(res)?.role==="seller")
              navigate('/my-properties')
            else if(JSON.parse(res)?.email && JSON.parse(res)?.role==="buyer")
            {
              setLoggedIn(true)
              setRole("buyer")
            }
            else{
              navigate('/login')
            }
          }
          else{
            navigate('/login')
          }
  }, []);

  useEffect(() => {
    var res=localStorage.getItem("loginData");
    if(res)
      var token = JSON.parse(res)?.token
    fetch('http://localhost:5189/api/Property/GetProperties',{
      method: "GET",
      headers: {
        "Authorization": "Bearer "+token,
      },
    })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setProperties(data);
      });
  }, []);

  // useEffect(() => {
  //   console.log(properties);
  // }, [properties]);

  
  return (loggedIn && role === "buyer" && (
    <div className="flex sm:h-screen justify-start items-center space-x-2 flex-wrap space-y-2">
      {properties.map((property: any, i: number) => (
        <div key={i} >
          <PropertyCard propertyData={property} />
        </div>
      ))}
    </div>
  ));
};

export default ViewProperty;
