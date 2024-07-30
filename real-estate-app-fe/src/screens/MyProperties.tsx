import React, { useEffect, useRef, useState } from "react";
import PropertyCard from "../components/Property-card.tsx";
import { useNavigate } from "react-router-dom";

const ViewProperty = () => {
  const navigate = useNavigate()
  const [properties, setProperties] = useState([]);
  const [loggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState("");
  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const propertiesRef:any = useRef([]);
  

  useEffect(() => {
    fetch('http://localhost:5189/api/Property/GetProperties')
      .then((res) => res.json())
      .then((data) => {
        const filteredProperties = data.filter((property: any) => property.userEmail === email);
        setProperties(filteredProperties);
      });
  }, []);

  // useEffect(() => {
  //   console.log(properties);
  // }, [properties]);

  useEffect(() => {
    var res = localStorage.getItem("loginData")
          if(res){

            if(JSON.parse(res)?.email && JSON.parse(res)?.role==="seller")
            {
              setLoggedIn(true)
              setRole("seller")
            setEmail(JSON.parse(res)?.email)
            }
            else if(JSON.parse(res)?.email && JSON.parse(res)?.role==="buyer")
            {
              
              setRole("buyer")
              navigate('/view-properties')
            }
            else{
              navigate('/login')
            }
          }
          else{
            navigate('/login')
          }
  }, []);

  

  
  return (loggedIn && role==="seller"&&(<div className=" flex justify-between">
    {properties?.map((property:any, i:any)=>(
      <div key={i} className="flex flex-wrap space-x-2 space-y-2">
        <PropertyCard propertyData={property}/>
      </div>

    ))}
  </div>));
};

export default ViewProperty;
