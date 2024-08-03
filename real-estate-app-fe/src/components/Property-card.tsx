import React, { useEffect } from 'react';
import { PropertyDataType } from '../data/propertyData.ts';
import cn from 'clsx';
import { Link } from 'react-router-dom';

const PropertyCard = ({ propertyData, className }: { propertyData: any, className?: string }) => {
  

  return (
    <Link to={`http://localhost:3000/view-property/${propertyData.pId}`} >
    <div className={cn('w-64 max-w-sm shadow-xl h-96 rounded-xl m-2 p-4 flex flex-col bg-white', className)}>
      <div className='w-full h-48 overflow-hidden rounded-t-lg'>
      {propertyData?.media?.map((mediaItem: any, index: number) => (
                  <div key={index} className="mb-4">
                    {mediaItem.type === "image/jpeg" || mediaItem.type ==="image/png" ? (
                      <img
                        src={mediaItem.url}
                        alt={`media-${index}`}
                        className="w-full h-56 object-cover rounded-lg"
                      />
                    ) : (
                      <video
                        src={mediaItem.url}
                        controls
                        className="w-full h-56 object-fill rounded-lg"
                      />
                    )}
                  </div>
                ))}
     {/* <img
          src={`${propertyData?.media[0].url}`}
          alt={propertyData.name}
          className="w-full h-full object-cover"
          onError={(e) => { e.currentTarget.src = '/placeholder.jpg'; }} 
        /> */}
      </div>
      <div className='w-full flex flex-col sm:p-2'>
        <div className='flex justify-between mb-2 gap-x-2'>
          <span className='text-lg md:text-nowrap font-bold'>{propertyData.name}</span>
          <span className='text-lg font-bold'>${propertyData.price}</span>
        </div>
        <div className='mb-2'>
          <span className='text-sm text-gray-600'>{propertyData.location}</span>
        </div>
       
          
          {propertyData.commercialSubtype && (
            <div className='flex space-x-2'>
            <span className='px-2 py-1 bg-green-100 text-green-700 rounded-full text-xs'>
              {propertyData.commercialSubtype}
            </span>
            <span className='px-2 py-1 bg-blue-100 text-blue-700 rounded-full text-xs'>
            {propertyData?.propertyDetails?.commercialAreaInSqFt} sq.ft
          </span>
            </div>
          )}
          {propertyData.residentialSubtype && (
            <div className='flex space-x-2'>
            <span className='px-2 py-1 bg-green-100 text-green-700 rounded-full text-xs'>
              {propertyData.residentialSubtype}
            </span>
            <span className='px-2 py-1 bg-blue-100 text-blue-700 rounded-full text-xs'>
            {propertyData?.propertyDetails?.areaInSqFt} sq.ft
          </span>
            </div>
          )}
        
      </div>
    </div>
    </Link>
  );
};

export default PropertyCard;
