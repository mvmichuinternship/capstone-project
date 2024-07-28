import React, { ReactNode } from 'react'
import cn from 'clsx'

const Card = ({children, className} : {children : ReactNode, className?:string}) => {
  return (
    <div className={cn('w-[80%] rounded-md shadow-md sm:p-10 p-2 space-y-2 sm:w-[50%]',className)}> 
    {children}
    </div>
  )
}

export default Card