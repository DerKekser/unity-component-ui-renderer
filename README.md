# Unity - Component UI Renderer

Unity - Component UI Renderer is a component-based framework for Unity, inspired by React and Tailwind CSS. It allows developers to build UI in Unity using a component-based approach, similar to how UI is built in React. The styling system is inspired by Tailwind CSS, a utility-first CSS framework.

## Features

- **React-Like Component System**: Define UI components as classes that inherit from a base `UIComponent` class. Components have lifecycle methods such as `OnMount` and `OnRender`, similar to React's lifecycle methods.

- **Tailwind CSS-Inspired Styling**: Define styles for components using the `StyleProps` class. The use of class names like "flex-grow-1" and "mt-5" is reminiscent of Tailwind's naming conventions.

For a more detailed look at these features, please refer to the [Todo Example Project](https://github.com/DerKekser/unity-component-ui-renderer/tree/master/Assets/Examples/Todo).

### Install

#### Install via Unity Package

Download the latest [release](https://github.com/DerKekser/unity-component-ui-renderer/releases) and import the package into your Unity project.
#### Install via git URL

You can add this package to your project by adding this git URL in the Package Manager:
```
https://github.com/DerKekser/unity-component-ui-renderer.git?path=Assets/Kekser/ComponentSystem
```
![Package Manager](/Assets/Kekser/Screenshots/package_manager.png)
### License

This library is under the MIT License.