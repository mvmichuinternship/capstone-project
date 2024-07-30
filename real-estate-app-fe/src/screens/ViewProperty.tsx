import React, { useEffect, useRef, useState } from "react";
import PropertyCard from "../components/Property-card.tsx";
import { useNavigate } from "react-router-dom";

const ViewProperty = () => {
  const navigate = useNavigate()
  const [properties, setProperties] = useState([]);
  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const propertiesRef:any = useRef([]);
  

  useEffect(() => {
    fetch('http://localhost:5189/api/Property/GetProperties')
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setProperties(data);
      });
  }, []);

  // useEffect(() => {
  //   console.log(properties);
  // }, [properties]);

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

  
    // const fetchProperties = async () => {
    //   try {
    //     const response = await fetch("http://localhost:5189/api/Property/GetProperties", {
    //       method: "GET",
    //       headers: {
    //         "Content-Type": "application/json",
    //       },
    //     });

    //     if (!response.ok) {
    //       throw new Error("Failed to fetch properties.");
    //     }

    //     const data = await response.json();
    //     console.log("Fetched data:", data);
    //     if(Array.isArray(data)){
    //     //   properties.push(data[0]);
    //     // setProperties(prevProperties => {
    //     //     const updatedProperties:any = [...prevProperties];
    //     //     updatedProperties.push(...data);
    //     //     return updatedProperties;
    //     // });          
    //     properties.push(...data);
    //     console.log("if")
    //     } 
    //     else{
    //       console.log("first");
    //     }
    //     console.log(properties)
    //   } catch (error) {
    //     setError(error.message);
    //   } 
    // // const response = await fetch('http://localhost:5189/api/Property/GetProperties');
    // // const json = await response.json();
    // // setProperties(json);
    // // console.log(properties)
    // };
    

  // useEffect(() => {
  //   fetchProperties();
  // }, []);
  
  return (loggedIn && role==="buyer"&&(<div className=" flex justify-between">
    {properties?.map((property:any, i:any)=>(
      <div key={i} className="flex flex-wrap space-x-2 space-y-2">
        <PropertyCard propertyData={property}/>
      </div>

    ))}
  </div>));
};

export default ViewProperty;
