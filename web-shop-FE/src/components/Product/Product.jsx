import React from "react";
import classes from "./Product.module.css";

const Product = ({ product, onDeleteProduct, onUpdateProduct }) => {  

  return (
    <div className={classes.productItem}>
      <div className={classes.namePriceContainer}>
        <h3>{product.name}</h3>
        <p className={classes.price}>Price: ${product.price}</p>
      </div>
      <button
        className={classes.updateButton}
        onClick={() => onUpdateProduct(product.id)}
      >
        Update this product
      </button>
      <button
        className={classes.deleteButton}
        onClick={() => onDeleteProduct(product.id)}
      >
        X
      </button>
      <img
        className={classes.picture} alt='Default'
        src={product.image ? `data:image/jpg;base64,${product.image}` : "defaultProduct.jpg"}
      />
      <p className={classes.amount}>In stock: {product.amount}</p>
      <p className={classes.description}>Description: {product.description}</p>
    </div>
  );
};

export default Product;