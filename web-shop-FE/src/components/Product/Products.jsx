import React, { useEffect, useState } from "react";
import sellerService from "../../services/sellerService";
import Product from "./Product";
import classes from "./Products.module.css";
import { useNavigate } from "react-router-dom";

const Products = () => {
  const [products, setProducts] = useState([]);

  const navigator = useNavigate();

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const fetchedProducts = await sellerService.getProducts();
      setProducts(fetchedProducts);
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const handleDeleteProduct = async (id) => {
    try {
      await sellerService.deleteProduct(id);
      fetchProducts();
    } catch (error) {
      if (error.response) {
        alert(error.response.data.Exception);
      }
    }
  };

  const updateProductHandler = async (id) => {
    navigator("/update_product/"+id);
  };

  return (
    <div>
      <h1 className={classes.title}>My Products</h1>
      <div className={classes.container}>
        {products.map((product) => (
          <Product
            key={product.id}
            onDeleteProduct={handleDeleteProduct}
            onUpdateProduct={updateProductHandler}
            product={product}
          />
        ))}
      </div>
    </div>
  );
};

export default Products;